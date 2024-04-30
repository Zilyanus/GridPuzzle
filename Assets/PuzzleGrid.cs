using System;
using UnityEngine;

public abstract class PuzzleGrid : MonoBehaviour
{ 
    public ICommand Command;
    public Transform MainObject;

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
}