using ProgrammingApp;

namespace TestProgrammingApp.TestBoard;

public class BoardTest
{
    [Theory]
    [InlineData(1, 2, 1, 2)]
    [InlineData(3, 4, 2, 2)]
    [InlineData(-1, -2, 0, 0)]
    public void TestPositionSetting(int x, int y, int xExpected, int yExpected)
    {
        var board = new Board() { Position = new Position(x, y) };
        var expected = new Position(xExpected, yExpected);
        Assert.Equivalent(expected, board.Position);
    }
}