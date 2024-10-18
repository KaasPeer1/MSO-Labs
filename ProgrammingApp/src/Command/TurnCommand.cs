namespace ProgrammingApp;

public class TurnCommand : ICommand
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

    public void Execute(Board board)
    {

        board.Direction = _turnDirection switch
        {
            "left" => board.Direction switch
            {
                Direction.North => Direction.West,
                Direction.West => Direction.South,
                Direction.South => Direction.East,
                Direction.East => Direction.North
            },
            "right" => board.Direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North
            },
            _ => throw new ArgumentException("Invalid turn direction")
        };
    }

    public override string ToString()
    {
        return $"Turn {_turnDirection}";
    }
}