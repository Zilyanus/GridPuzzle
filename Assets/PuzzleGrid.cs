using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleGrid : SerializedMonoBehaviour
{ 
    public ICommand Command;
    public Dictionary<Vector3,Transform> MainObjects;

    public static event Action<ICommand> OnPuzzleGridTriggered;

    public void ExecuteGrid()
    {
        if (Command != null && ControlCommand())
        {
            OnPuzzleGridTriggered.Invoke(Command);
        }
        else
        {
            Debug.Log("FalseControl");
        }
    }

    public abstract void CreateCommand(Vector2 ComingDir);

    public abstract bool ControlCommand();

    public void GetMainObject(Transform newObject, Vector3 Dir)
    {
        MainObjects.Add(Dir,newObject);  
    }
}