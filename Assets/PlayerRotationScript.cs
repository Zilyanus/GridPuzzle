using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationScript : MonoBehaviour
{
    private void OnEnable()
    {
        RotateCommand.OnGridRotate += RotateThePlayer; 
    }

    private void OnDisable()
    {
        RotateCommand.OnGridRotate -= RotateThePlayer;
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
        transform.DORotate(Vector3.zero, 0.3f);
    }
}
