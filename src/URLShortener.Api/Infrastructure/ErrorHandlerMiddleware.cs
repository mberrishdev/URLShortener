﻿using System.Net;
using System.Text.Json;
using URLShortener.Application.Exceptions;
using URLShortener.Domain.Exceptions;
using ApplicationException = System.ApplicationException;
using CommandValidationException = URLShortener.Application.Exceptions.CommandValidationException;

namespace URLShortener.Api.Infrastructure;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = error switch
            {
                ObjectNotFoundException => (int)HttpStatusCode.NotFound,
                NotValidException => (int)HttpStatusCode.BadRequest,
                ObjectAlreadyExistException => (int)HttpStatusCode.Conflict,
                CommandValidationException => (int)HttpStatusCode.BadRequest,
                ApplicationException => (int)HttpStatusCode.BadRequest,
                Domain.Exceptions.CommandValidationException => (int)HttpStatusCode.BadRequest,
                DomainException => (int)HttpStatusCode.BadRequest,
                TaskCanceledException => (int)HttpStatusCode.GatewayTimeout,
                not null => (int)HttpStatusCode.InternalServerError,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new { error.Message });
            if (response.StatusCode == 500)
                result = JsonSerializer.Serialize(new { error.Message, error.StackTrace });

            await response.WriteAsync(result);
        }
    }
}