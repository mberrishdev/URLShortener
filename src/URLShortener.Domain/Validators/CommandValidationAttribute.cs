namespace URLShortener.Domain.Validators;

[AttributeUsage(AttributeTargets.Class)]
public class CommandValidationAttribute : Attribute
{
    public CommandValidationAttribute(Type validatoType)
    {
        ValidorType = validatoType;
        ValidorName = validatoType.Name;
    }

    public required Type ValidorType { get; set; }
    public required string ValidorName { get; set; }
}