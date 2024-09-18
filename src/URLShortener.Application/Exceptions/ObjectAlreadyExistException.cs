namespace URLShortener.Application.Exceptions;

public class ObjectAlreadyExistException : ApplicationException
{
    public ObjectAlreadyExistException(string message) : base(
        $"{message}")
    {
    }
}