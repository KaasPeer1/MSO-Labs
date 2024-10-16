namespace ProgrammingApp;

public class Board
{
    private Position _position = new (0, 0);

    public Board(int size = 3, Direction direction = Direction.East)
    {
        Size = size;
        Direction = direction;
    }

    public Position Position
    {
        get => _position;
        set => _position = ClampToBoard(value);
    }

    public Direction Direction { get; set; }
    public int Size { get; }

    public override string ToString()
    {
        return $"{_position} facing {Direction.ToString().ToLower()}";
    }

    private Position ClampToBoard(Position position)
    {
        return new Position(
            Math.Clamp(position.X, 0, Size - 1),
            Math.Clamp(position.Y, 0, Size - 1)
        );
    }
}