using EduCode.Board;
using EduCode.Command;
using EduCode.Location;

namespace UnitTests.TestCommand;

public class TurnCommandTest
{
    [Theory]
    [InlineData(Direction.North, "right", Direction.East)]
    [InlineData(Direction.East, "right", Direction.South)]
    [InlineData(Direction.South, "right", Direction.West)]
    [InlineData(Direction.West, "right", Direction.North)]
    [InlineData(Direction.North, "left", Direction.West)]
    [InlineData(Direction.West, "left", Direction.South)]
    [InlineData(Direction.South, "left", Direction.East)]
    [InlineData(Direction.East, "left", Direction.North)]
    public void TestExecute(Direction initialDirection, string turnDirection, Direction expectedDirection)
    {
        var board = new EduBoard(3) { Direction = initialDirection };
        var command = new TurnCommand(turnDirection);

        command.Execute(board);

        Assert.Equal(expectedDirection, board.Direction);
    }
}