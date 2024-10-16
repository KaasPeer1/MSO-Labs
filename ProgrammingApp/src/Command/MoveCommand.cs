namespace ProgrammingApp;

public class MoveCommand : ICommand
{
    private readonly int _amount;

    public MoveCommand(int amount)
    {
        _amount = amount;
    }

    public void Execute(Board board)
    {
        board.Position += Vector.FromDirection(board.Direction) * _amount;
    }

    public override string ToString()
    {
        return $"Move {_amount}";
    }
}