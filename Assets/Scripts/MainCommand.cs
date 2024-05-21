using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Sequence sequence = DOTween.Sequence();
        for (int i = _commandList.Count - 1; i >= 0; i--)
        {
            Debug.Log(_commandList.Count + " " + i);

            sequence.Append(DOVirtual.DelayedCall(i == _commandList.Count - 1 ? 0 : _commandList[i + 1].ReturnExecutionTime(),()=> _commandList[i].Undo()));
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

    IEnumerator UndoFor()
    {
        for (int i = _commandList.Count - 1; i >= 0; i--)
        {
            _commandList[i].Undo();
            yield return new WaitForSeconds(_commandList[i].ReturnExecutionTime());
        }
    }
}
