using System.ComponentModel;
using System.Runtime.CompilerServices;
using EduCode.Model.Exceptions;
using EduCode.Model.Location;

namespace EduCode.Model.Board;

public class EduBoard : INotifyPropertyChanged
{
    private Position _position = new(0, 0);
    private Direction _direction = Direction.East;
    private int _size;
    private List<Position> _walls;
    private Position? _endPosition;

    public EduBoard(int size, IEnumerable<Position>? wallPositions, Position? endPosition = null)
    {
        Size = size;
        _walls = wallPositions?.ToList() ?? new List<Position>();
        _endPosition = endPosition;
    }

    public void Reset(int size = -1, IEnumerable<Position>? wallPositions = null, Position? endPosition = null)
    {
        if (size != -1)
        {
            if (size < 1)
            {
                throw new ArgumentException("Board size must be at least 1.");
            }
            Size = size;
        }

        Position = new Position(0, 0);
        Direction = Direction.East;

        if (wallPositions != null) Walls = wallPositions.ToList();
        if (endPosition != null) EndPosition = endPosition;
    }

    public Position Position
    {
        get => _position;
        set
        {
            if (IsOutOfGrid(value)) throw new PositionOutOfGridException("You can't go there!");
            if (IsWallAt(value)) throw new PositionIsWallException("You can't go there!");
            SetField(ref _position, value);
        }
    }

    public Direction Direction
    {
        get => _direction;
        set => SetField(ref _direction, value);
    }

    public int Size
    {
        get => _size;
        set => SetField(ref _size, value);
    }

    public List<Position> Walls
    {
        get => _walls;
        set => SetField(ref _walls, value);
    }

    public Position? EndPosition
    {
        get => _endPosition;
        set => SetField(ref _endPosition, value);
    }

    public bool IsWallAhead()
    {
        var ahead = Position + Vector.FromDirection(Direction);
        return IsWallAt(ahead);
    }

    public bool IsGridEdgeAhead()
    {
        var ahead = Position + Vector.FromDirection(Direction);
        return IsOutOfGrid(ahead);
    }

    private bool IsWallAt(Position position)
    {
        return Walls.Contains(position);
    }

    private bool IsOutOfGrid(Position position)
    {
        return position.X < 0 || position.X >= Size || position.Y < 0 || position.Y >= Size;
    }

    public bool IsInEndPosition()
    {
        if (_endPosition == null) return false;
        return (Position.Equals(_endPosition.Value));
    }

    public override string ToString()
    {
        return $"{_position} facing {Direction.ToString().ToLower()}";
    }

    #region INotifyPropertyChanged implementation

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    #endregion
}