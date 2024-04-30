using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public AnimationCurve pressedTımeSpeedCurve;
    [SerializeField] PlayerInputs InputActions;

    [SerializeField] float Speed = 0.3f;

    PlayerSurrounding playerSurrounding;

    GridParent gridParent;

    Animator animator;
    MainCommand LastCommand;

    private IDictionary<Vector3, float> movementTimeSpeedMap = new Dictionary<Vector3, float>
        {
            {Vector3.right, 0f},
            {Vector3.left, 0f},
            {Vector3.up, 0f},
            {Vector3.down, 0f},
        };

    [SerializeField] LayerMask GridMask;

    //MoveInvoker moveInvoker;

    public static event Action OnMoved;
    public static event Action OnUndoPressed;
    public static event Action OnRedoPressed;

    bool isStartAnimationEnded;

    bool CantMove;
    bool isPaused;
    bool isGameFinished;

    public static event Action<MainCommand> MoveCommandAction;

    List<PuzzleGrid> puzzleGrids = new List<PuzzleGrid>();

    bool isPressedX = false;
    bool isPressedY = false;
    private void Awake()
    {
        InputActions = new PlayerInputs();
        InputActions.Player.Enable();
    }

    private void OnEnable()
    {
        GameManager.OnGameFinished.AddListener(OnGameFinished);
        PauseManager.OnPausedChanged.AddListener(OnPausedChange);
    }

    private void OnDisable()
    {
        GameManager.OnGameFinished.RemoveListener(OnGameFinished);
        PauseManager.OnPausedChanged.RemoveListener(OnPausedChange);
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        //moveInvoker = new MoveInvoker();

        playerSurrounding = GetComponent<PlayerSurrounding>();

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, Vector2.down,0.1f, GridMask);

        if (hit)
        {
            if (hit.transform.parent != null)
            {
                gridParent = hit.transform.GetComponentInParent<GridParent>();
            }
            else
            {
                gridParent = hit.transform.GetComponent<GridScript>().TransformToGridParent();
            }

            transform.parent = gridParent.transform;
        }

        StartAnimationScript.StartingAnimationEnded.AddListener(() => { isStartAnimationEnded = true; }); 
    }

    // Update is called once per frame
    void Update()
    {
        CantMove = isPaused || isGameFinished;

        Vector2 Movement = (isStartAnimationEnded && !CantMove) ? InputActions.Player.Movement.ReadValue<Vector2>() : Vector2.zero;



        if (Movement.x < 0)
        {
            transform.localScale = new Vector3(-1f, 0.9f, 1);
        }
        else if (Movement.x > 0)
        {
            transform.localScale = new Vector3(1f, 0.9f, 1);
        }
        else
        {
            animator.SetFloat("X", 0);
            isPressedX = false;
        }

        if (Movement.y == 0)
        {
            animator.SetFloat("Y", 0);
            isPressedY = false;
        } 
        

        if (Movement.x > 0 && !isPressedX)
        {
            isPressedX = true;
            MoveControl(2, "X", Movement.x, Vector3.right);
        }
        else if (Movement.x < 0 && !isPressedX)
        {
            isPressedX = true;
            MoveControl(3, "X", Movement.x, Vector3.left);
        }
        else if (Movement.y > 0 && !isPressedY)
        {
            isPressedY = true;
            MoveControl(0, "Y", Movement.y, Vector3.up);
        }
        else if (Movement.y < 0 && !isPressedY)
        {
            isPressedY = true;
            MoveControl(1, "Y", Movement.y, Vector3.down);
        }

        if (InputActions.Player.UndoButton.ReadValue<float>() != 0 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
        {
            OnUndoPressed.Invoke();
        }
        else if (InputActions.Player.RedoButton.ReadValue<float>() != 0 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
        {
            OnRedoPressed.Invoke();
        }

        if (InputActions.Player.RestartA.ReadValue<float>() != 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void MoveControl(int index,string Key, float AnimatorValue, Vector3 Dir)
    {
        MainCommand _mainCommand = new MainCommand();
        puzzleGrids.Clear();

        if (playerSurrounding.GetSurroundAtIndex(index) == 1 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
        {
            animator.SetFloat(Key, AnimatorValue);
            ICommand moveCommand = new MoveCommand(this, Dir, transform);
            _mainCommand.AddCommand(moveCommand);
            LastCommand = _mainCommand;
        }
        else if (playerSurrounding.GetSurroundAtIndex(index) == 0 && gridParent.GetSurroundAtIndex(index) == 0 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
        {
            animator.SetFloat(Key, AnimatorValue);
            ICommand moveCommand = new MoveCommand(this, Dir, gridParent.transform);
            _mainCommand.AddCommand(moveCommand);
            LastCommand = _mainCommand;
        }
        else if ((playerSurrounding.GetSurroundAtIndex(index) == 0 || playerSurrounding.GetSurroundAtIndex(index) == 4) && gridParent.GetSurroundAtIndex(index) == 4 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
        {
            animator.SetFloat(Key, AnimatorValue);
            ICommand moveCommand = new MoveCommand(this, Dir, gridParent.transform);
            _mainCommand.AddCommand(moveCommand);
            LastCommand = _mainCommand;

            PuzzleGrid puzzleGrid = gridParent.GetPuzzleGridAtIndex(index);

            if (!puzzleGrids.Contains(puzzleGrid))
            {
                puzzleGrids.Add(puzzleGrid);
                puzzleGrid.CreateCommand(Dir);
            }
        }
        else
        {
            //Debug.Log(playerSurrounding.GetSurroundAtIndex(index) + " " + gridParent.GetSurroundAtIndex(index));
        }
    }
    
    public void Move(Vector3 dir, Transform transform)
    {
        movementTimeSpeedMap[dir] += Time.deltaTime;
        if (!DOTween.IsTweening(transform))
        {
            OnMoved.Invoke();
            //transform.DOMove(transform.position + dir, EvalSpeed(movementTimeSpeedMap[dir])).OnComplete(() => { transform.GetComponent<SurroundControl>().ControlSurround(); });
            transform.DOMove(transform.position + dir, 0.332f).SetEase(Ease.InOutCubic).OnComplete(() => 
            {                
                transform.GetComponent<SurroundControl>().ControlSurround();
                if (puzzleGrids.Count > 0)
                {
                    for (int i = 0; i < puzzleGrids.Count; i++)
                    {
                        puzzleGrids[i].ExecuteGrid();
                    }
                        puzzleGrids.Clear();
                }                 
            });
            movementTimeSpeedMap[dir] = 0;
        }
    }

    public void ConfirmMovement()
    {
        if (LastCommand != null)
        {
            if (MoveCommandAction != null)
                MoveCommandAction.Invoke(LastCommand);
            LastCommand = null;
        }
    }

    private float EvalSpeed(float presseDuratıon)
    {
        return Speed / Mathf.Clamp(pressedTımeSpeedCurve.Evaluate(presseDuratıon), 1f, 15f);
    }

    void OnPausedChange(bool value)
    {
        isPaused = value;
    }

    void OnGameFinished()
    {
        isGameFinished = true;
    }
}
