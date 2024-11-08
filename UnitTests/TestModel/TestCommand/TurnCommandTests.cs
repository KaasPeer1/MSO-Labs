using EduCode.Model.Board;
using EduCode.Model.Command;
using EduCode.Model.Location;

namespace UnitTests.TestModel.TestCommand;

public class TurnCommandTests
{
    [Theory]
    [InlineData(Direction.North, "left", Direction.West)]
    [InlineData(Direction.West, "left", Direction.South)]
    [InlineData(Direction.South, "left", Direction.East)]
    [InlineData(Direction.East, "left", Direction.North)]
    [InlineData(Direction.North, "right", Direction.East)]
    [InlineData(Direction.East, "right", Direction.South)]
    [InlineData(Direction.South, "right", Direction.West)]
    [InlineData(Direction.West, "right", Direction.North)]
    public void Execute_ChangesDirectionCorrectly(Direction initialDirection, string turnDirection, Direction expectedDirection)
    {
        // Arrange
        var board = new EduBoard(5, null) { Direction = initialDirection };
        var trace = new List<Position>();
        var command = new TurnCommand(turnDirection);

        // Act
        command.Execute(board, ref trace);

        // Assert
        Assert.Equal(expectedDirection, board.Direction);
    }

    [Fact]
    public void Constructor_InvalidTurnDirection_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new TurnCommand("invalid"));
    }
}