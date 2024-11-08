namespace EduCode.Model.Exceptions;

public class PositionOutOfGridException : Exception
{
    public PositionOutOfGridException()
    {
    }

    public PositionOutOfGridException(string message) : base(message)
    {
    }

    public PositionOutOfGridException(string message, Exception innerException) : base(message, innerException)
    {
    }
}