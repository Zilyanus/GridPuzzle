using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MoveInvoker
{
    ICommand latestCommand;
    Stack<ICommand> _commandList;
    Stack<ICommand> _undoCommandList;

    public MoveInvoker()
    {
        _commandList = new Stack<ICommand>();
        _undoCommandList = new Stack<ICommand>();
    }

    public void AddCommand(ICommand newCommand)
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
