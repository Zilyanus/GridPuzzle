using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Direction
{
    North,South,West,East
}

public class PlayerMovement : MonoBehaviour
{
    public AnimationCurve pressedTımeSpeedCurve;
    [SerializeField] PlayerInputs InputActions;

    [SerializeField] float Speed = 0.3f;

    private IDictionary<Direction, float> movementTimeSpeedMap = new Dictionary<Direction, float>
        {
            {Direction.East, 0f},
            {Direction.West, 0f},
            {Direction.North, 0f},
            {Direction.South, 0f},
        };

    private void Awake()
    {
        InputActions = new PlayerInputs();
        InputActions.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Movement = InputActions.Player.Movement.ReadValue<Vector2>();

        if (Movement.x > 0)
        {
            Move(Direction.East, transform.position + transform.right);
        }
        else if (Movement.x < 0)
        {
            Move(Direction.West, transform.position - transform.right);
        }
        else if (Movement.y > 0)
        {               
            Move(Direction.North, transform.position + transform.up);
        }
        else if (Movement.y < 0)
        {
            Move(Direction.South, transform.position - transform.up);
        }
    }
    
    void Move(Direction direction,Vector3 EndValue)
    {
        movementTimeSpeedMap[direction] += Time.deltaTime;
        if (!DOTween.IsTweening(transform))
        {
            transform.DOMove(EndValue, EvalSpeed(movementTimeSpeedMap[direction]));
            movementTimeSpeedMap[direction] = 0;
        }
    }

    private float EvalSpeed(float presseDuratıon)
    {
        return Speed / pressedTımeSpeedCurve.Evaluate(presseDuratıon);
    }
}
