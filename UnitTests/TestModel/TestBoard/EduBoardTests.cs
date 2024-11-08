using EduCode.Model.Board;
using EduCode.Model.Exceptions;
using EduCode.Model.Location;

namespace UnitTests.TestModel.TestBoard;

public class EduBoardTests
{
    [Fact]
    public void Reset_WithValidSize_UpdatesSize()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var newSize = 10;

        // Act
        board.Reset(newSize);

        // Assert
        Assert.Equal(newSize, board.Size);
    }

    [Fact]
    public void Reset_WithInvalidSize_ThrowsArgumentException()
    {
        // Arrange
        var board = new EduBoard(5, null);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => board.Reset(0));
    }

    [Fact]
    public void Reset_WithWallPositions_UpdatesWalls()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var newWalls = new List<Position> { new Position(1, 1), new Position(2, 2) };

        // Act
        board.Reset(wallPositions: newWalls);

        // Assert
        Assert.Equal(newWalls, board.Walls);
    }

    [Fact]
    public void Reset_WithEndPosition_UpdatesEndPosition()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var newEndPosition = new Position(4, 4);

        // Act
        board.Reset(endPosition: newEndPosition);

        // Assert
        Assert.Equal(newEndPosition, board.EndPosition);
    }

    [Fact]
    public void Reset_ResetsPositionAndDirection()
    {
        // Arrange
        var board = new EduBoard(5, null);
        board.Position = new Position(3, 3);
        board.Direction = Direction.North;

        // Act
        board.Reset();

        // Assert
        Assert.Equal(new Position(0, 0), board.Position);
        Assert.Equal(Direction.East, board.Direction);
    }

    [Fact]
    public void Position_SetValidPosition_UpdatesPosition()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var newPosition = new Position(2, 2);

        // Act
        board.Position = newPosition;

        // Assert
        Assert.Equal(newPosition, board.Position);
    }

    [Fact]
    public void Position_SetPositionOutOfGrid_ThrowsPositionOutOfGridException()
    {
        // Arrange
        var board = new EduBoard(5, null);
        var invalidPosition = new Position(5, 5);

        // Act & Assert
        Assert.Throws<PositionOutOfGridException>(() => board.Position = invalidPosition);
    }

    [Fact]
    public void Position_SetPositionOnWall_ThrowsPositionIsWallException()
    {
        // Arrange
        var wallPositions = new List<Position> { new Position(1, 1) };
        var board = new EduBoard(5, wallPositions);
        var wallPosition = new Position(1, 1);

        // Act & Assert
        Assert.Throws<PositionIsWallException>(() => board.Position = wallPosition);
    }

    [Fact]
    public void WallAhead_NoWallAhead_ReturnsFalse()
    {
        // Arrange
        var board = new EduBoard(5, null);
        board.Position = new Position(2, 2);
        board.Direction = Direction.North;

        // Act
        var result = board.WallAhead();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void WallAhead_WallAhead_ReturnsTrue()
    {
        // Arrange
        var wallPositions = new List<Position> { new Position(2, 1) };
        var board = new EduBoard(5, wallPositions);
        board.Position = new Position(2, 2);
        board.Direction = Direction.North;

        // Act
        var result = board.WallAhead();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GridEdge_NotAtEdge_ReturnsFalse()
    {
        // Arrange
        var board = new EduBoard(5, null);
        board.Position = new Position(2, 2);
        board.Direction = Direction.North;

        // Act
        var result = board.GridEdge();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void GridEdge_AtEdge_ReturnsTrue()
    {
        // Arrange
        var board = new EduBoard(5, null);
        board.Position = new Position(0, 0);
        board.Direction = Direction.West;

        // Act
        var result = board.GridEdge();

        // Assert
        Assert.True(result);
    }
}