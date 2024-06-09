using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CommandInvoker : SerializedMonoBehaviour
{
    [SerializeField] MainCommand latestCommand;
    public Stack<MainCommand> _commandList = new Stack<MainCommand>();
    public Stack<MainCommand> _undoCommandList = new Stack<MainCommand>();
    private void OnEnable()
    {
        PlayerMovement.MoveCommandAction += AddCommand;
        GridManager.CombineCommandAction += GetCombineCommand;

        PlayerMovement.OnUndoPressed += UndoCommand;
        PlayerMovement.OnRedoPressed += RedoCommand;

        PuzzleGrid.OnPuzzleGridTriggered += AddExistingCommand;

        CollectableScript.OnCommandCreated += AddExistingCommand;
    }

    private void OnDisable()
    {
        PlayerMovement.MoveCommandAction -= AddCommand;
        GridManager.CombineCommandAction -= GetCombineCommand;

        PlayerMovement.OnUndoPressed -= UndoCommand;
        PlayerMovement.OnRedoPressed -= RedoCommand;

        PuzzleGrid.OnPuzzleGridTriggered -= AddExistingCommand;

        CollectableScript.OnCommandCreated -= AddExistingCommand;
    }

    public void GetCombineCommand(ICommand newCommand)
    {
        AddExistingCommand(newCommand);
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
        newCommand.AddMono(this);
        newCommand.Execute();
    }

    public void UndoCommand()
    {
        if (_commandList.Count > 0)
        {
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
