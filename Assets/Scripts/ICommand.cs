public interface ICommand
{
    public void Execute()
    {

    }

    public void Undo()
    {
        
    }

    public void Redo()
    {

    }

    public float ReturnExecutionTime()
    {
        return 0;
    }
}
