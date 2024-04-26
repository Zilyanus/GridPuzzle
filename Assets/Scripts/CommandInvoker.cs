using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    MainCommand latestCommand;
    Stack<MainCommand> _commandList = new Stack<MainCommand>();
    Stack<MainCommand> _undoCommandList = new Stack<MainCommand>();

    private void OnEnable()
    {
        PlayerMovement.MoveCommandAction += AddCommand;
        GridManager.CombineCommandAction += AddExistingCommand;

        PlayerMovement.OnUndoPressed += UndoCommand;
        PlayerMovement.OnRedoPressed += RedoCommand;

        PuzzleGrid.OnPuzzleGridTriggered += AddExistingCommand;
    }

    private void OnDisable()
    {
        PlayerMovement.MoveCommandAction -= AddCommand;
        GridManager.CombineCommandAction -= AddExistingCommand;

        PlayerMovement.OnUndoPressed -= UndoCommand;
        PlayerMovement.OnRedoPressed -= RedoCommand;

        PuzzleGrid.OnPuzzleGridTriggered -= AddExistingCommand;
    }

    public void AddExistingCommand(ICommand newCommand)
    {
        latestCommand = _commandList.Pop();
        latestCommand.AddCommand(newCommand);
        _commandList.Push(latestCommand);
        newCommand.Execute();
    }

    public void AddCommand(MainCommand newCommand)
    {
        _undoCommandList.Clear();
        _commandList.Push(newCommand);
        newCommand.Execute();
    }

    public void UndoCommand()
    {
        if (_commandList.Count > 0)
        {
            latestCommand = _commandList.First();
            if (!latestCommand.Undo())
            {
                return;
            }
            latestCommand = _commandList.Pop();
            _undoCommandList.Push(latestCommand);
            latestCommand.Undo();
        }
    }

    public void RedoCommand()
    {
        if (_undoCommandList.Count > 0)
        {
            latestCommand = _undoCommandList.Pop();
            _commandList.Push(latestCommand);
            latestCommand.Redo();
        }
    }
}