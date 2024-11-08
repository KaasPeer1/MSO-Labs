namespace EduCode.Model.Exceptions;

public class PositionIsWallException : Exception
{
    public PositionIsWallException()
    {
    }

    public PositionIsWallException(string message) : base(message)
    {
    }

    public PositionIsWallException(string message, Exception innerException) : base(message, innerException)
    {
    }
}