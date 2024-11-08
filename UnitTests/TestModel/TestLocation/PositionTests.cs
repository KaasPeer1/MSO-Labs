using EduCode.Model.Location;

namespace UnitTests.TestModel.TestLocation;

public class PositionTests
{
    [Fact]
    public void Operator_Addition_ReturnsCorrectResult()
    {
        // Arrange
        var position = new Position(2, 3);
        var vector = new Vector(1, 1);

        // Act
        var result = position + vector;

        // Assert
        Assert.Equal(new Position(3, 4), result);
    }

    [Fact]
    public void Operator_Subtraction_ReturnsCorrectResult()
    {
        // Arrange
        var position = new Position(2, 3);
        var vector = new Vector(1, 1);

        // Act
        var result = position - vector;

        // Assert
        Assert.Equal(new Position(1, 2), result);
    }

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        // Arrange
        var position = new Position(2, 3);

        // Act
        var result = position.ToString();

        // Assert
        Assert.Equal("(2,3)", result);
    }

    [Fact]
    public void Equals_ReturnsTrueForEqualPositions()
    {
        // Arrange
        var position1 = new Position(2, 3);
        var position2 = new Position(2, 3);

        // Act
        var result = position1.Equals(position2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Equals_ReturnsFalseForDifferentPositions()
    {
        // Arrange
        var position1 = new Position(2, 3);
        var position2 = new Position(3, 2);

        // Act
        var result = position1.Equals(position2);

        // Assert
        Assert.False(result);
    }
}