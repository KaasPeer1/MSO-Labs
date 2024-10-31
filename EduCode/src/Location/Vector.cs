namespace EduCode.Location;

public readonly struct Vector
{
    public Vector(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; }

    public int Y { get; }

    public static Vector FromDirection(Direction direction) => direction switch
    {
        Direction.North => new Vector(0, -1),
        Direction.East => new Vector(1, 0),
        Direction.South => new Vector(0, 1),
        Direction.West => new Vector(-1, 0),
        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
    };

    public static Vector operator -(Vector vec) => new Vector(-vec.X, -vec.Y);
    public static Vector operator +(Vector vec1, Vector vec2) => new Vector(vec1.X + vec2.X, vec1.Y + vec2.Y);
    public static Vector operator -(Vector vec1, Vector vec2) => new Vector(vec1.X - vec2.X, vec1.Y - vec2.Y);
    public static Vector operator *(Vector vec, int scalar) => new Vector(vec.X * scalar, vec.Y * scalar);
    public static Vector operator *(int scalar, Vector vec) => new Vector(vec.X * scalar, vec.Y * scalar);
}