namespace URLShortener.Application.Exceptions;

public class ObjectNotFoundException : ApplicationException
{
    public ObjectNotFoundException(string entityName, string propertyName, object propertyValue) : base(
        $"{entityName} with the {propertyName}: {propertyValue} not found.")
    {
    }

    public ObjectNotFoundException(string message) : base(message)
    {
    }
}