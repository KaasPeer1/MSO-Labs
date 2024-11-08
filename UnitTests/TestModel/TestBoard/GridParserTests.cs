using EduCode.Model.Board;
using EduCode.Model.Location;

namespace UnitTests.TestModel.TestBoard;

public class GridParserTests
{
    [Fact]
    public void ParseFromString_ValidInput_ParsesCorrectly()
    {
        // Arrange
        string input = @"
                +ooo
                o+oo
                ooxo
                oooo";

        // Act
        GridParser.ParseFromString(input, out int size, out List<Position> walls, out Position? endPosition);

        // Assert
        Assert.Equal(4, size);
        Assert.Contains(new Position(0, 0), walls);
        Assert.Contains(new Position(1, 1), walls);
        Assert.Equal(new Position(2, 2), endPosition);
    }

    [Fact]
    public void ParseFromString_EmptyInput_ParsesCorrectly()
    {
        // Arrange
        string input = "";

        // Act
        GridParser.ParseFromString(input, out int size, out List<Position> walls, out Position? endPosition);

        // Assert
        Assert.Equal(0, size);
        Assert.Empty(walls);
        Assert.Null(endPosition);
    }

    [Fact]
    public void ParseFromString_NoEndPosition_ParsesCorrectly()
    {
        // Arrange
        string input = @"
                +ooo
                o+oo
                oooo
                oooo";

        // Act
        GridParser.ParseFromString(input, out int size, out List<Position> walls, out Position? endPosition);

        // Assert
        Assert.Equal(4, size);
        Assert.Contains(new Position(0, 0), walls);
        Assert.Contains(new Position(1, 1), walls);
        Assert.Null(endPosition);
    }

    [Fact]
    public void ParseFromString_NoWalls_ParsesCorrectly()
    {
        // Arrange
        string input = @"
                oooo
                oooo
                ooxo
                oooo";

        // Act
        GridParser.ParseFromString(input, out int size, out List<Position> walls, out Position? endPosition);

        // Assert
        Assert.Equal(4, size);
        Assert.Empty(walls);
        Assert.Equal(new Position(2, 2), endPosition);
    }

    [Fact]
    public void ParseFromString_InvalidCharacter_ThrowsFormatException()
    {
        // Arrange
        string input = @"
                +ooo
                o+oo
                oozo
                oooo";

        // Act & Assert
        Assert.Throws<FormatException>(() => GridParser.ParseFromString(input, out int size, out List<Position> walls, out Position? endPosition));
    }
}