using EduCode.Model.Board;
using EduCode.Model.Location;

namespace EduCode.Model.Command;

public class MoveCommand : IEduCommand
{
    private readonly int _amount;

    public MoveCommand(int amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount must be non-negative.");
        }
        _amount = amount;
    }

    public int MaximumDepth => 0;

    public void Execute(EduBoard board, ref List<Position> trace)
    {
        var remaining = _amount;
        while (remaining > 0)
        {
            board.Position += Vector.FromDirection(board.Direction);
            trace.Add(board.Position);
            remaining--;
        }
    }

    public override string ToString()
    {
        return $"Move {_amount}";
    }
}