using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerInputs InputActions;

    [SerializeField] float Speed = 0.3f;
    private void Awake()
    {
        InputActions = new PlayerInputs();
        InputActions.Player.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Movement = InputActions.Player.Movement.ReadValue<Vector2>();
        if (!DOTween.IsTweening(transform))
        {
            if (Movement.x > 0)
            {
                transform.DOMoveX(transform.position.x + 1, Speed);
            }
            else if (Movement.x < 0)
            {
                transform.DOMoveX(transform.position.x - 1, Speed);
            }
            else if (Movement.y > 0)
            {
                transform.DOMoveY(transform.position.y + 1, Speed);
            }
            else if (Movement.y < 0)
            {
                transform.DOMoveY(transform.position.y - 1, Speed);
            }
        }
    }
}
