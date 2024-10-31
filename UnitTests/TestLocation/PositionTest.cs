using EduCode.Location;

namespace UnitTests.TestLocation;

public class PositionTest
{
    [Theory]
    [InlineData(1, 2, 3, 4, 4, 6)]
    [InlineData(1, 2, -3, -4, -2, -2)]
    [InlineData(1, 2, 0, 0, 1, 2)]
    public void TestAddition(int x1, int y1, int x2, int y2, int x, int y)
    {
        var pos1 = new Position(x1, y1);
        var pos2 = new Vector(x2, y2);
        var expected = new Position(x, y);
        Assert.Equivalent(expected, pos1 + pos2);
    }

    [Theory]
    [InlineData(1, 2, 3, 4, -2, -2)]
    [InlineData(1, 2, -3, -4, 4, 6)]
    [InlineData(1, 2, 0, 0, 1, 2)]
    public void TestSubtraction(int x1, int y1, int x2, int y2, int x, int y)
    {
        var pos1 = new Position(x1, y1);
        var pos2 = new Vector(x2, y2);
        var expected = new Position(x, y);
        Assert.Equivalent(expected, pos1 - pos2);
    }
}