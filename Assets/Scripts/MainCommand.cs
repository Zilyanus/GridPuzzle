using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCommand : ICommand
{
    [SerializeField] List<ICommand> _commandList = new List<ICommand>();

    public void Execute()
    {
        for (int i = 0; i < _commandList.Count; i++)
        {
            _commandList[i].Execute();
        }
    }

    public bool Undo()
    {
        for (int i = _commandList.Count - 1; i >= 0; i--)
        {
            _commandList[i].Undo();
        }
        return true;
    }

    public void Redo()
    {
        for (int i = 0; i < _commandList.Count; i++)
        {
            _commandList[i].Redo();
        }
    }

    public void AddCommand(ICommand command)
    {
        _commandList.Add(command);
    }
}
