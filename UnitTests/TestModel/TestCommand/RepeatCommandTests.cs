using EduCode.Model.Board;
using EduCode.Model.Command;
using EduCode.Model.Location;
using Moq;

namespace UnitTests.TestModel.TestCommand;

public class RepeatCommandTests
{
    [Fact]
    public void Execute_ExecutesCommandsCorrectNumberOfTimes()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var trace = new List<Position>();
        var mockCommand = new Mock<IEduCommand>();
        var commands = new List<IEduCommand> { mockCommand.Object };
        var repeatCommand = new RepeatCommand(3, commands);

        // Act
        repeatCommand.Execute(board, ref trace);

        // Assert
        mockCommand.Verify(c => c.Execute(board, ref trace), Times.Exactly(3));
    }

    [Fact]
    public void Execute_UpdatesTraceCorrectly()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var trace = new List<Position>();
        var mockCommand = new Mock<IEduCommand>();
        mockCommand.Setup(c => c.Execute(board, ref trace)).Callback(() => trace.Add(new Position(1, 1)));
        var commands = new List<IEduCommand> { mockCommand.Object };
        var repeatCommand = new RepeatCommand(2, commands);

        // Act
        repeatCommand.Execute(board, ref trace);

        // Assert
        Assert.Equal(2, trace.Count);
        Assert.Equal(new Position(1, 1), trace[0]);
        Assert.Equal(new Position(1, 1), trace[1]);
    }

    [Fact]
    public void Execute_WithNoCommands_DoesNothing()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var trace = new List<Position>();
        var repeatCommand = new RepeatCommand(3, new List<IEduCommand>());

        // Act
        repeatCommand.Execute(board, ref trace);

        // Assert
        Assert.Empty(trace);
    }
}