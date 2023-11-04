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

    private IDictionary<Vector3, float> movementTimeSpeedMap = new Dictionary<Vector3, float>
        {
            {Vector3.right, 0f},
            {Vector3.left, 0f},
            {Vector3.up, 0f},
            {Vector3.down, 0f},
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
            Move(Vector3.right);
        }
        else if (Movement.x < 0)
        {
            Move(Vector3.left);
        }
        else if (Movement.y > 0)
        {               
            Move(Vector3.up);
        }
        else if (Movement.y < 0)
        {
            Move(Vector3.down);
        }
    }
    
    void Move(Vector3 EndValue)
    {
        movementTimeSpeedMap[EndValue] += Time.deltaTime;
        if (!DOTween.IsTweening(transform))
        {
            transform.DOMove(transform.position + EndValue, EvalSpeed(movementTimeSpeedMap[EndValue]));
            movementTimeSpeedMap[EndValue] = 0;
        }
    }

    private float EvalSpeed(float presseDuratıon)
    {
        return Speed / pressedTımeSpeedCurve.Evaluate(presseDuratıon);
    }
}
