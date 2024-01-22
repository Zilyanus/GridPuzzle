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
}
