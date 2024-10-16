namespace ProgrammingApp;

public readonly struct Position
{
    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }
    public int Y { get; }

    public static Position operator +(Position pos, Vector vec) => new Position(pos.X + vec.X, pos.Y + vec.Y);
    public static Position operator -(Position pos, Vector vec) => new Position(pos.X - vec.X, pos.Y - vec.Y);

    public override string ToString() => $"({X},{Y})";
}