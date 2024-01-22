using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public AnimationCurve pressedTımeSpeedCurve;
    [SerializeField] PlayerInputs InputActions;

    [SerializeField] float Speed = 0.3f;

    PlayerSurrounding playerSurrounding;

    GridParent gridParent;

    private IDictionary<Vector3, float> movementTimeSpeedMap = new Dictionary<Vector3, float>
        {
            {Vector3.right, 0f},
            {Vector3.left, 0f},
            {Vector3.up, 0f},
            {Vector3.down, 0f},
        };

    [SerializeField] LayerMask GridMask;

    MoveInvoker moveInvoker;

    private void Awake()
    {
        InputActions = new PlayerInputs();
        InputActions.Player.Enable();
    }

    private void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Movement = InputActions.Player.Movement.ReadValue<Vector2>();

        if (Movement.x > 0)
        {
            if (playerSurrounding.GetSurroundAtIndex(2) == 1 && !DOTween.IsTweening(gridParent.transform) && !DOTween.IsTweening(transform))
            {
                ICommand moveCommand = new MoveCommand(this, Vector3.right, transform);
                moveInvoker.AddCommand(moveCommand);
            }
            if (playerSurrounding.GetSurroundAtIndex(2) == 0 && gridParent.GetSurroundAtIndex(2) == 0 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                ICommand moveCommand = new MoveCommand(this, Vector3.right, gridParent.transform);
                moveInvoker.AddCommand(moveCommand);
            }
        }
        else if (Movement.x < 0)
        {
            if (playerSurrounding.GetSurroundAtIndex(3) == 1 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                ICommand moveCommand = new MoveCommand(this, Vector3.left, transform);
                moveInvoker.AddCommand(moveCommand);
            }
            if (playerSurrounding.GetSurroundAtIndex(3) == 0 && gridParent.GetSurroundAtIndex(3) == 0 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                ICommand moveCommand = new MoveCommand(this, Vector3.left, gridParent.transform);
                moveInvoker.AddCommand(moveCommand);
            }
        }
        else if (Movement.y > 0)
        {
            if (playerSurrounding.GetSurroundAtIndex(0) == 1 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                ICommand moveCommand = new MoveCommand(this, Vector3.up, transform);
                moveInvoker.AddCommand(moveCommand);
            }
            if (playerSurrounding.GetSurroundAtIndex(0) == 0 && gridParent.GetSurroundAtIndex(0) == 0 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                ICommand moveCommand = new MoveCommand(this, Vector3.up, gridParent.transform);
                moveInvoker.AddCommand(moveCommand);
            }
        }
        else if (Movement.y < 0)
        {
            if (playerSurrounding.GetSurroundAtIndex(1) == 1 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                ICommand moveCommand = new MoveCommand(this, Vector3.down, transform);
                moveInvoker.AddCommand(moveCommand);
            }
            if (playerSurrounding.GetSurroundAtIndex(1) == 0 && gridParent.GetSurroundAtIndex(1) == 0 && !DOTween.IsTweening(transform) && !DOTween.IsTweening(gridParent.transform))
            {
                ICommand moveCommand = new MoveCommand(this, Vector3.down, gridParent.transform);
                moveInvoker.AddCommand(moveCommand);
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
            transform.DOMove(transform.position + dir, EvalSpeed(movementTimeSpeedMap[dir])).OnComplete(() => { transform.GetComponent<SurroundControl>().ControlSurround(); });
            movementTimeSpeedMap[dir] = 0;
        }
    }

    private float EvalSpeed(float presseDuratıon)
    {
        return Speed / Mathf.Clamp(pressedTımeSpeedCurve.Evaluate(presseDuratıon), 1f, 15f);
    }
}
