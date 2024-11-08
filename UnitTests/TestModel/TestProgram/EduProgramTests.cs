using EduCode.Model.Board;
using EduCode.Model.Command;
using EduCode.Model.Location;
using EduCode.Model.Program;
using Moq;

namespace UnitTests.TestModel.TestProgram;

public class EduProgramTests
{
    [Fact]
    public void TextualTrace_ReturnsCorrectTrace()
    {
        // Arrange
        var commands = new List<IEduCommand>
        {
            new MoveCommand(5),
            new TurnCommand("left"),
            new MoveCommand(2)
        };
        var program = new EduProgram(commands);

        // Act
        var result = program.TextualTrace;

        // Assert
        Assert.Equal("Move 5, Turn left, Move 2.", result);
    }

    [Fact]
    public void TextualTrace_ReturnsEmptyStringForNoCommands()
    {
        // Arrange
        var program = new EduProgram(new List<IEduCommand>());

        // Act
        var result = program.TextualTrace;

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public void CommandCount_ReturnsCorrectCount()
    {
        // Arrange
        var commands = new List<IEduCommand>
        {
            new MoveCommand(5),
            new TurnCommand("left"),
            new MoveCommand(2)
        };
        var program = new EduProgram(commands);

        // Act
        var result = program.CommandCount;

        // Assert
        Assert.Equal(3, result);
    }

    [Fact]
    public void CommandCount_ReturnsZeroForNoCommands()
    {
        // Arrange
        var program = new EduProgram(new List<IEduCommand>());

        // Act
        var result = program.CommandCount;

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void MaximumDepth_ReturnsCorrectDepth()
    {
        // Arrange
        var commands = new List<IEduCommand>
        {
            new MoveCommand(5),
            new RepeatCommand(2, new List<IEduCommand>
            {
                new MoveCommand(1),
                new TurnCommand("left")
            })
        };
        var program = new EduProgram(commands);

        // Act
        var result = program.MaximumDepth;

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void MaximumDepth_ReturnsZeroForNoCommands()
    {
        // Arrange
        var program = new EduProgram(new List<IEduCommand>());

        // Act
        var result = program.MaximumDepth;

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Run_ExecutesAllCommands()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var trace = new List<Position>();
        var mockCommand1 = new Mock<IEduCommand>();
        var mockCommand2 = new Mock<IEduCommand>();
        var commands = new List<IEduCommand> { mockCommand1.Object, mockCommand2.Object };
        var program = new EduProgram(commands);

        // Act
        program.Run(board, ref trace);

        // Assert
        mockCommand1.Verify(c => c.Execute(board, ref trace), Times.Once);
        mockCommand2.Verify(c => c.Execute(board, ref trace), Times.Once);
    }

    [Fact]
    public void Run_AddsInitialPositionToTrace()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var trace = new List<Position>();
        var mockCommand = new Mock<IEduCommand>();
        var commands = new List<IEduCommand> { mockCommand.Object };
        var program = new EduProgram(commands);

        // Act
        program.Run(board, ref trace);

        // Assert
        Assert.Contains(board.Position, trace);
    }

    [Fact]
    public void ToString_ReturnsCorrectStringRepresentation()
    {
        // Arrange
        var commands = new List<IEduCommand>
        {
            new MoveCommand(5),
            new TurnCommand("left"),
            new RepeatCommand(2, new List<IEduCommand>
            {
                new MoveCommand(1),
                new TurnCommand("right")
            })
        };
        var program = new EduProgram(commands);
        var expectedString = "Move 5\nTurn left\nRepeat 2 times\n    Move 1\n    Turn right\n";

        // Act
        var result = program.ToString();

        // Assert
        Assert.Equal(expectedString, result);
    }
}