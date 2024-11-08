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

    public Position[] Execute(EduBoard board)
    {
        var remaining = _amount;
        while (!board.IsWallAhead() && remaining > 0)
        {
            board.Position += Vector.FromDirection(board.Direction);
            remaining--;
        }
        return new[] {board.Position};
    }

    public override string ToString()
    {
        return $"Move {_amount}";
    }
}