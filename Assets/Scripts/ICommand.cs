public interface ICommand
{
    public void Execute()
    {

    }

    public bool Undo()
    {
        return true;
    }

    public void Redo()
    {

    }

    public float ReturnExecutionTime()
    {
        return 0;
    }
}
