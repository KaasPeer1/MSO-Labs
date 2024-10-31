using EduCode.Location;

namespace UnitTests.TestLocation;

public class VectorTest
{
    [Theory]
    [InlineData(Direction.North, 0, -1)]
    [InlineData(Direction.East, 1, 0)]
    [InlineData(Direction.South, 0, 1)]
    [InlineData(Direction.West, -1, 0)]
    public void TestFromDirection(Direction direction, int x, int y)
    {
        var expected = new Vector(x, y);
        Assert.Equivalent(expected, Vector.FromDirection(direction));
    }

    [Theory]
    [InlineData(1, 2, 3, 4, 4, 6)]
    [InlineData(1, 2, -3, -4, -2, -2)]
    [InlineData(1, 2, 0, 0, 1, 2)]
    public void TestAddition(int x1, int y1, int x2, int y2, int xExpected, int yExpected)
    {
        var vec1 = new Vector(x1, y1);
        var vec2 = new Vector(x2, y2);
        var expected = new Vector(xExpected, yExpected);
        Assert.Equivalent(expected, vec1 + vec2);
    }

    [Theory]
    [InlineData(1, 2, 3, 4, -2, -2)]
    [InlineData(1, 2, -3, -4, 4, 6)]
    [InlineData(1, 2, 0, 0, 1, 2)]
    public void TestSubtraction(int x1, int y1, int x2, int y2, int xExpected, int yExpected)
    {
        var vec1 = new Vector(x1, y1);
        var vec2 = new Vector(x2, y2);
        var expected = new Vector(xExpected, yExpected);
        Assert.Equivalent(expected, vec1 - vec2);
    }

    [Theory]
    [InlineData(1, 2, 3, 3, 6)]
    [InlineData(1, 2, -3, -3, -6)]
    [InlineData(1, 2, 0, 0, 0)]
    public void TestMultiplication(int x, int y, int scalar, int xExpected, int yExpected)
    {
        var vec = new Vector(x, y);
        var expected = new Vector(xExpected, yExpected);
        Assert.Equivalent(expected, vec * scalar);
    }
}