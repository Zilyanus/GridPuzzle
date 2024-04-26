using System;
using UnityEngine;

public abstract class PuzzleGrid : MonoBehaviour
{
    public ICommand Command;
    public Transform MainObject;

    public static event Action<ICommand> OnPuzzleGridTriggered;

    public void ExecuteGrid()
    {
        if (Command != null)
        {
            OnPuzzleGridTriggered.Invoke(Command);
        }
    }

    public abstract void CreateCommand();
}