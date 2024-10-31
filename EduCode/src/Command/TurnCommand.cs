using EduCode.Board;
using EduCode.Location;

namespace EduCode.Command;

public class TurnCommand : IEduCommand
{
    private readonly string _turnDirection;

    public TurnCommand(string turnDirection)
    {
        if (turnDirection != "left" && turnDirection != "right")
        {
            throw new ArgumentException("Invalid turn direction");
        }
        _turnDirection = turnDirection;
    }

    public int MaximumDepth => 0;

    public void Execute(EduBoard board)
    {

        board.Direction = _turnDirection switch
        {
            "left" => board.Direction switch
            {
                Direction.North => Direction.West,
                Direction.West => Direction.South,
                Direction.South => Direction.East,
                Direction.East => Direction.North,
                _ => throw new ArgumentException("Invalid board direction")
            },
            "right" => board.Direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => throw new ArgumentException("Invalid board direction")
            },
            _ => throw new ArgumentException("Invalid turn direction")
        };
    }

    public override string ToString()
    {
        return $"Turn {_turnDirection}";
    }
}