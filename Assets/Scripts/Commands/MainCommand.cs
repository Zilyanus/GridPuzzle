using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainCommand : ICommand
{
    [SerializeField] List<ICommand> _commandList = new List<ICommand>();

    MonoBehaviour monoBehaviour;

    public void Execute()
    {
        for (int i = 0; i < _commandList.Count; i++)
        {
            _commandList[i].Execute();
        }
    }

    public void Undo()
    {
        monoBehaviour.StartCoroutine(UndoFor());
    }

    public void Redo()
    {
        monoBehaviour.StartCoroutine(RedoFor());
    }

    public void AddCommand(ICommand command)
    {
        _commandList.Add(command);
    }

    public void AddMono(MonoBehaviour NewMonoBehaviour)
    {
        if (monoBehaviour == null)
            monoBehaviour = NewMonoBehaviour;
    }

    IEnumerator UndoFor()
    {
        for (int i = _commandList.Count - 1; i >= 0; i--)
        {
            _commandList[i].Undo();
            yield return new WaitForSeconds(_commandList[i].ReturnExecutionTime());
        }
    }

    IEnumerator RedoFor()
    {
        for (int i = 0; i < _commandList.Count; i++)
        {
            _commandList[i].Redo();
            if (i != 0)
            {
                RemoveCommand(_commandList[i]);
            }

            yield return new WaitForSeconds(_commandList[i].ReturnExecutionTime());
        }
    }

    void RemoveCommand(ICommand command)
    {
        _commandList.Remove(command);
    }
}
