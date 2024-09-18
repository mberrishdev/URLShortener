using System;

namespace URLShortener.Domain.Validators;

[AttributeUsage(AttributeTargets.Class)]
public class CommandValidationAttribute : Attribute
{
    public required Type ValidorType { get; set; }
    public required string ValidorName { get; set; }

    public CommandValidationAttribute(Type validatoType)
    {
        ValidorType = validatoType;
        ValidorName = validatoType.Name;
    }
}