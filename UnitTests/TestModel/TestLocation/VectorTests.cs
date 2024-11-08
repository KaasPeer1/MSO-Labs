using EduCode.Model.Location;

namespace UnitTests.TestModel.TestLocation;

public class VectorTests
{
    [Theory]
    [InlineData(Direction.North, 0, -1)]
    [InlineData(Direction.East, 1, 0)]
    [InlineData(Direction.South, 0, 1)]
    [InlineData(Direction.West, -1, 0)]
    public void FromDirection_ReturnsCorrectVector(Direction direction, int expectedX, int expectedY)
    {
        // Act
        var result = Vector.FromDirection(direction);

        // Assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }

    [Fact]
    public void FromDirection_InvalidDirection_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var invalidDirection = (Direction)999;

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => Vector.FromDirection(invalidDirection));
    }

    [Fact]
    public void Operator_Negation_ReturnsCorrectResult()
    {
        // Arrange
        var vector = new Vector(2, 3);

        // Act
        var result = -vector;

        // Assert
        Assert.Equal(new Vector(-2, -3), result);
    }

    [Fact]
    public void Operator_Addition_ReturnsCorrectResult()
    {
        // Arrange
        var vector1 = new Vector(2, 3);
        var vector2 = new Vector(1, 1);

        // Act
        var result = vector1 + vector2;

        // Assert
        Assert.Equal(new Vector(3, 4), result);
    }

    [Fact]
    public void Operator_Subtraction_ReturnsCorrectResult()
    {
        // Arrange
        var vector1 = new Vector(2, 3);
        var vector2 = new Vector(1, 1);

        // Act
        var result = vector1 - vector2;

        // Assert
        Assert.Equal(new Vector(1, 2), result);
    }

    [Fact]
    public void Operator_MultiplicationWithScalar_ReturnsCorrectResult()
    {
        // Arrange
        var vector = new Vector(2, 3);
        var scalar = 2;

        // Act
        var result1 = vector * scalar;
        var result2 = scalar * vector;

        // Assert
        Assert.Equal(new Vector(4, 6), result1);
        Assert.Equal(new Vector(4, 6), result2);
    }
}