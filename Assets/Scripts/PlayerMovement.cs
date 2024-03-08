using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{
    public AnimationCurve pressedTımeSpeedCurve;
    [SerializeField] PlayerInputs InputActions;

    [SerializeField] float Speed = 0.3f;

    PlayerSurrounding playerSurrounding;

    GridParent gridParent;

    Animator animator;
    ICommand LastCommand;

    private IDictionary<Vector3, float> movementTimeSpeedMap = new Dictionary<Vector3, float>
        {
            {Vector3.right, 0f},
            {Vector3.left, 0f},
            {Vector3.up, 0f},
            {Vector3.down, 0f},
        };

    [SerializeField] LayerMask GridMask;

    MoveInvoker moveInvoker;

    public static event Action OnMoved;

    bool isStartAnimationEnded;

    bool CantMove;
    bool isPaused;
    bool isGameFinished;

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
        moveInvoker = new MoveInvoker();

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
        }

        if (Movement.y == 0)
        {
            animator.SetFloat("Y", 0);
        } 
        

        if (Movement.x > 0)
        {
            if (playerSurrounding.GetSurroundAtIndex(2) == 1 && !DOTween.IsTweening(gridParent.transform) && !DOTween.IsTweening(transform))
            {
                animator.SetFloat("X", Movement.x);
                ICommand moveCommand = new MoveCommand(this, Vector3.right, transform);
                LastCommand = moveCommand;
            }
            if (playerSurrounding.GetSurroundAtIndex(2) == 0 && gridParent.GetSurroundAtIndex(2) == 0 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                animator.SetFloat("X", Movement.x);
                ICommand moveCommand = new MoveCommand(this, Vector3.right, gridParent.transform);
                LastCommand = moveCommand;
            }
        }
        else if (Movement.x < 0)
        {
            if (playerSurrounding.GetSurroundAtIndex(3) == 1 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                animator.SetFloat("X", Movement.x);
                ICommand moveCommand = new MoveCommand(this, Vector3.left, transform);
                LastCommand = moveCommand;
            }
            if (playerSurrounding.GetSurroundAtIndex(3) == 0 && gridParent.GetSurroundAtIndex(3) == 0 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                animator.SetFloat("X", Movement.x);
                ICommand moveCommand = new MoveCommand(this, Vector3.left, gridParent.transform);
                LastCommand = moveCommand;
            }
        }
        else if (Movement.y > 0)
        {
            if (playerSurrounding.GetSurroundAtIndex(0) == 1 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                animator.SetFloat("Y", Movement.y);
                ICommand moveCommand = new MoveCommand(this, Vector3.up, transform);
                LastCommand = moveCommand;
            }
            if (playerSurrounding.GetSurroundAtIndex(0) == 0 && gridParent.GetSurroundAtIndex(0) == 0 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                animator.SetFloat("Y", Movement.y);
                ICommand moveCommand = new MoveCommand(this, Vector3.up, gridParent.transform);
                LastCommand = moveCommand;
            }
        }
        else if (Movement.y < 0)
        {
            if (playerSurrounding.GetSurroundAtIndex(1) == 1 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                animator.SetFloat("Y", Movement.y);
                ICommand moveCommand = new MoveCommand(this, Vector3.down, transform);
                LastCommand = moveCommand;
            }
            if (playerSurrounding.GetSurroundAtIndex(1) == 0 && gridParent.GetSurroundAtIndex(1) == 0 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                animator.SetFloat("Y", Movement.y);
                ICommand moveCommand = new MoveCommand(this, Vector3.down, gridParent.transform);
                LastCommand = moveCommand;
            }
        }

        if (InputActions.Player.UndoButton.ReadValue<float>() != 0 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
        {
            moveInvoker.UndoCommand();
        }
        else if (InputActions.Player.RedoButton.ReadValue<float>() != 0 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
        {
            moveInvoker.RedoCommand();
        }

        if (InputActions.Player.RestartA.ReadValue<float>() != 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void Move(Vector3 dir, Transform transform)
    {
        movementTimeSpeedMap[dir] += Time.deltaTime;
        if (!DOTween.IsTweening(transform))
        {
            OnMoved.Invoke();
            //transform.DOMove(transform.position + dir, EvalSpeed(movementTimeSpeedMap[dir])).OnComplete(() => { transform.GetComponent<SurroundControl>().ControlSurround(); });
            transform.DOMove(transform.position + dir, 0.332f).SetEase(Ease.InOutCubic).OnComplete(() => { transform.GetComponent<SurroundControl>().ControlSurround(); });
            movementTimeSpeedMap[dir] = 0;
        }
    }

    public void ConfirmMovement()
    {
        if (LastCommand != null)
        {
            moveInvoker.AddCommand(LastCommand);
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
