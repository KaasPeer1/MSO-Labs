using EduCode.Model.Program;

namespace UnitTests.TestModel.TestProgram;

public class ProgramParserTests
{
    [Fact]
    public void ParseString_ReturnsNullForEmptyString()
    {
        // Arrange
        string program = string.Empty;

        // Act
        var result = ProgramParser.ParseString(program);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ParseString_ReturnsNullForWhitespaceString()
    {
        // Arrange
        string program = "   ";

        // Act
        var result = ProgramParser.ParseString(program);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ParseString_ReturnsEduProgramForValidProgram()
    {
        // Arrange
        string program = "Move 5\nTurn left\nRepeat 2 times\n    Move 1\n    Turn right\n";

        // Act
        var result = ProgramParser.ParseString(program);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<EduProgram>(result);
        Assert.Equal(3, result.CommandCount);
    }

    [Fact]
    public void ParseString_ThrowsFormatExceptionForInvalidCommand()
    {
        // Arrange
        string program = "InvalidCommand 5";

        // Act & Assert
        Assert.Throws<FormatException>(() => ProgramParser.ParseString(program));
    }

    [Fact]
    public void ParseString_ThrowsFormatExceptionForInvalidIndentation()
    {
        // Arrange
        string program = "Move 5\n  Turn left";

        // Act & Assert
        Assert.Throws<FormatException>(() => ProgramParser.ParseString(program));
    }
}