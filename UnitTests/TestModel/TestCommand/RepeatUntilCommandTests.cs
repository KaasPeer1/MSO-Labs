using EduCode.Model.Board;
using EduCode.Model.Command;
using EduCode.Model.Condition;
using EduCode.Model.Location;
using Moq;

namespace UnitTests.TestModel.TestCommand;

public class RepeatUntilCommandTests
{
    [Fact]
    public void Execute_ExecutesCommandsUntilConditionIsMet()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var trace = new List<Position>();
        var mockCondition = new Mock<IEduCondition>();
        var mockCommand = new Mock<IEduCommand>();
        var commands = new List<IEduCommand> { mockCommand.Object };
        var repeatUntilCommand = new RepeatUntilCommand(mockCondition.Object, commands);

        mockCondition.SetupSequence(c => c.Evaluate(board))
            .Returns(false)
            .Returns(false)
            .Returns(true);

        // Act
        repeatUntilCommand.Execute(board, ref trace);

        // Assert
        mockCommand.Verify(c => c.Execute(board, ref trace), Times.Exactly(2));
    }

    [Fact]
    public void Execute_UpdatesTraceCorrectly()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var trace = new List<Position>();
        var mockCondition = new Mock<IEduCondition>();
        var mockCommand = new Mock<IEduCommand>();
        mockCommand.Setup(c => c.Execute(board, ref trace)).Callback(() => trace.Add(new Position(1, 1)));
        var commands = new List<IEduCommand> { mockCommand.Object };
        var repeatUntilCommand = new RepeatUntilCommand(mockCondition.Object, commands);

        mockCondition.SetupSequence(c => c.Evaluate(board))
            .Returns(false)
            .Returns(true);

        // Act
        repeatUntilCommand.Execute(board, ref trace);

        // Assert
        Assert.Single(trace);
        Assert.Equal(new Position(1, 1), trace[0]);
    }

    [Fact]
    public void Execute_WithConditionInitiallyMet_DoesNotExecuteCommands()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var trace = new List<Position>();
        var mockCondition = new Mock<IEduCondition>();
        var mockCommand = new Mock<IEduCommand>();
        var commands = new List<IEduCommand> { mockCommand.Object };
        var repeatUntilCommand = new RepeatUntilCommand(mockCondition.Object, commands);

        mockCondition.Setup(c => c.Evaluate(board)).Returns(true);

        // Act
        repeatUntilCommand.Execute(board, ref trace);

        // Assert
        mockCommand.Verify(c => c.Execute(board, ref trace), Times.Never);
    }
}