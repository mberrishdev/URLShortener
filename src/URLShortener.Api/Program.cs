using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using URLShortener.Api.Infrastructure.StartupConfiguration;
using Serilog;
using URLShortener.Application.Features.Urls.Queries;
using URLShortener.Domain.Entities.Urls.Commands;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

    builder.ConfigureService();

    var app = builder.Build();

    app.ConfigureMiddleware();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}