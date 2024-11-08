using EduCode.Model.Board;
using EduCode.Model.Command;
using EduCode.Model.Location;

namespace UnitTests.TestModel.TestCommand;

public class MoveCommandTests
{
    [Fact]
    public void Execute_MovesCorrectAmountOfSteps()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var trace = new List<Position>();
        var command = new MoveCommand(3);

        // Act
        command.Execute(board, ref trace);

        // Assert
        Assert.Equal(3, trace.Count);
        Assert.Equal(new Position(3, 0), board.Position);
    }

    [Fact]
    public void Execute_UpdatesTraceCorrectly()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var trace = new List<Position>();
        var command = new MoveCommand(2);

        // Act
        command.Execute(board, ref trace);

        // Assert
        Assert.Equal(2, trace.Count);
        Assert.Equal(new Position(1, 0), trace[0]);
        Assert.Equal(new Position(2, 0), trace[1]);
    }

    [Fact]
    public void Execute_HandlesZeroAmount()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var trace = new List<Position>();
        var command = new MoveCommand(0);

        // Act
        command.Execute(board, ref trace);

        // Assert
        Assert.Empty(trace);
        Assert.Equal(new Position(0, 0), board.Position);
    }

    [Fact]
    public void Execute_ThrowsExceptionForNegativeAmount()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => new MoveCommand(-1));
    }
}