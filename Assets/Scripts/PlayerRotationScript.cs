using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationScript : MonoBehaviour
{
    public bool isRotating;

    private void OnEnable()
    {
        RotateCommand.OnGridRotate += RotateThePlayer; 
        RotateCommand.OnStartGridRotate += OnStartGridRotate; 
    }

    private void OnDisable()
    {
        RotateCommand.OnGridRotate -= RotateThePlayer;
        RotateCommand.OnStartGridRotate -= OnStartGridRotate;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RotateThePlayer(float value)
    {
        transform.DORotate(Vector3.forward, 0.3f);
    }

    void OnStartGridRotate(bool value)
    {
        isRotating = value;
    }
}
