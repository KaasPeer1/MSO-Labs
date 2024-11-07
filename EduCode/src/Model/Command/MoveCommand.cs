using EduCode.Model.Board;
using EduCode.Model.Location;

namespace EduCode.Model.Command;

public class MoveCommand : IEduCommand
{
    private readonly int _amount;

    public MoveCommand(int amount)
    {
        _amount = amount;
    }

    public int MaximumDepth => 0;

    public Position[] Execute(EduBoard board)
    {
        board.Position += Vector.FromDirection(board.Direction) * _amount;
        return new[] {board.Position};
    }

    public override string ToString()
    {
        return $"Move {_amount}";
    }
}