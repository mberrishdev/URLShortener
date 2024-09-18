﻿namespace URLShortener.Application.Exceptions;

public class CommandValidationException : ApplicationException
{
    public CommandValidationException(string message) : base(message)
    {
    }
}