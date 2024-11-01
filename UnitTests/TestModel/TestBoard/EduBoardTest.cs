using EduCode.Model.Board;
using EduCode.Model.Location;

namespace UnitTests.TestModel.TestBoard;

public class EduBoardTest
{
    [Theory]
    [InlineData(1, 2, 1, 2)]
    [InlineData(3, 4, 2, 2)]
    [InlineData(-1, -2, 0, 0)]
    public void TestPositionSetting(int x, int y, int xExpected, int yExpected)
    {
        var board = new EduBoard(3) { Position = new Position(x, y) };
        var expected = new Position(xExpected, yExpected);
        Assert.Equivalent(expected, board.Position);
    }
}