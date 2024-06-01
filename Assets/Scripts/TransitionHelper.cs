using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionHelper : MonoBehaviour
{
    LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = GetComponentInParent<LevelManager>();    
    }

    public void OnAnimationFinished()
    {
        levelManager.ClosePanel();
    }
}
