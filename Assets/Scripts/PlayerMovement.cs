﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;
using ZilyanusLib.Audio;

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

    float movementTimeSpeed = 0;

    [SerializeField] LayerMask GridMask;

    public static event Action<int> OnMoved;
    public static event Action OnUndoPressed;
    public static event Action OnRedoPressed;

    bool isStartAnimationEnded;

    bool CantMove;
    bool isPaused;
    bool isGameFinished;

    public static event Action<MainCommand> MoveCommandAction;

    List<PuzzleGrid> puzzleGrids = new List<PuzzleGrid>();

    bool isMoving = false;

    [SerializeField] SoundData soundData;

    PlayerRotationScript playerRotationScript;

    bool PressedUndo;
    bool PressedRedo;
    private void Awake()
    {
        InputActions = new PlayerInputs();
        InputActions.Player.Enable();

        playerRotationScript = GetComponent<PlayerRotationScript>();    
    }

    private void OnEnable()
    {
        GameManager.OnGameFinished.AddListener(OnGameFinished);
        PauseManager.OnPausedChanged.AddListener(OnPausedChange);

        UndoController.OnUndoButtonPressed += UndoPressed;
        UndoController.OnRedoButtonPressed += RedoPressed;
        StartAnimationScript.StartingAnimationEnded.AddListener(() => { isStartAnimationEnded = true; });

    }

    private void OnDisable()
    {
        GameManager.OnGameFinished.RemoveListener(OnGameFinished);
        PauseManager.OnPausedChanged.RemoveListener(OnPausedChange);

        UndoController.OnUndoButtonPressed -= UndoPressed;
        UndoController.OnRedoButtonPressed -= RedoPressed;

        StartAnimationScript.StartingAnimationEnded.RemoveListener(() => { isStartAnimationEnded = true; });
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

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

        //StartAnimationScript.StartingAnimationEnded.AddListener(() => { isStartAnimationEnded = true; }); 
    }

    // Update is called once per frame
    void Update()
    {
        CantMove = isPaused || isGameFinished;

        Vector2 Movement = (isStartAnimationEnded && !CantMove && !playerRotationScript.isRotating) ? InputActions.Player.Movement.ReadValue<Vector2>() : Vector2.zero;

        Speed = EvalSpeed(movementTimeSpeed);

        animator.speed = Speed;

        if (Movement.x < 0)
        {
            transform.localScale = new Vector3(-1f, 0.9f, 1);
            movementTimeSpeed += Time.deltaTime;
        }
        else if (Movement.x > 0)
        {
            transform.localScale = new Vector3(1f, 0.9f, 1);
            movementTimeSpeed += Time.deltaTime;
        }
        else
        {
            animator.SetFloat("X", 0);
        }

        if (Movement.y == 0)
        {
            animator.SetFloat("Y", 0);
        }
        else
        {
            movementTimeSpeed += Time.deltaTime;
        }

        if (Movement  == Vector2.zero)
        {
            movementTimeSpeed = 0;
            isMoving = false;
        }
        

        if (Movement.x > 0 && !isMoving)
        {
            MoveControl(2, "X", Movement.x, Vector3.right);
        }
        else if (Movement.x < 0 && !isMoving)
        {
            MoveControl(3, "X", Movement.x, Vector3.left);
        }
        else if (Movement.y > 0 && !isMoving)
        {
            MoveControl(0, "Y", Movement.y, Vector3.up);
        }
        else if (Movement.y < 0 && !isMoving)
        {
            MoveControl(1, "Y", Movement.y, Vector3.down);
        }

        if (InputActions.Player.UndoButton.ReadValue<float>() != 0)
        {
            UndoPressed();
        }
        else if (InputActions.Player.RedoButton.ReadValue<float>() != 0)
        {
            RedoPressed();
        }

        if (InputActions.Player.UndoButton.ReadValue<float>() == 0)
            PressedUndo = false;

        if (InputActions.Player.RedoButton.ReadValue<float>() == 0)
            PressedRedo = false;

        if (InputActions.Player.RestartA.ReadValue<float>() != 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void MoveControl(int index,string Key, float AnimatorValue, Vector3 Dir)
    {
        MainCommand _mainCommand = new MainCommand();
        puzzleGrids.Clear();

        isMoving = true;

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
            Debug.Log(playerSurrounding.GetSurroundAtIndex(index) + " " + gridParent.GetSurroundAtIndex(index));
        }
    }
    
    public void Move(Vector3 dir, Transform transform, int value = 1)
    {
        AudioClass.PlayAudio(soundData);

        movementTimeSpeedMap[dir] += Time.deltaTime;
        if (!DOTween.IsTweening(transform))
        {
            OnMoved.Invoke(value);
            transform.DOMove(transform.position + dir, 0.332f / Speed).SetEase(Ease.InOutCubic).OnComplete(() => 
            {                
                transform.GetComponent<SurroundControl>().ControlSurround();
                if (puzzleGrids.Count > 0)
                {
                    animator.SetFloat("X", 0);
                    animator.SetFloat("Y", 0);

                    for (int i = 0; i < puzzleGrids.Count; i++)
                    {
                        puzzleGrids[i].ExecuteGrid();
                    }
                    puzzleGrids.Clear();
                    DOVirtual.DelayedCall(1.1f,()=> isMoving = false);
                }
                else
                {
                    isMoving = false;
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
        return Mathf.Clamp(pressedTımeSpeedCurve.Evaluate(presseDuratıon), 1f, 15f);
    }

    void OnPausedChange(bool value)
    {
        isPaused = value;
    }

    void OnGameFinished()
    {
        isGameFinished = true;
    }

    void UndoPressed()
    {
        if (!DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform) && !PressedUndo && !playerRotationScript.isRotating && !isGameFinished)
        {
            PressedUndo = true;
            OnUndoPressed.Invoke();
        }
    }

    void RedoPressed()
    {
        if (!DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform) && !PressedRedo && !playerRotationScript.isRotating && !isGameFinished)
        {
            PressedRedo = true;
            OnRedoPressed.Invoke();
        }
    }
}
