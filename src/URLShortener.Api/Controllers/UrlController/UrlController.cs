using MediatR;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Application.Exceptions;
using URLShortener.Application.Features.Urls.Queries;
using URLShortener.Domain.Entities.Urls.Commands;

namespace URLShortener.Api.Controllers.UrlController;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/[controller]")]
public class UrlController(IMediator mediator) : ApiControllerBase(mediator)
{
    /// <summary>
    /// Creates a new shortened URL.
    /// </summary>
    /// <param name="command">The CreateUrlCommand containing URL data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created shortened URL.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateUrlCommand command, CancellationToken cancellationToken)
    {
        command.Schema = HttpContext.Request.Scheme;
        command.Host = HttpContext.Request.Host.ToString();
        var result = await mediator.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Retrieves and redirects to the original URL.
    /// </summary>
    /// <param name="code">The shortened code for the URL.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Redirects to the original URL.</returns>
    [HttpGet("{code}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] string code, CancellationToken cancellationToken)
    {
        var query = new GetUrlByCodeQuery(code);
        var result = await mediator.Send(query, cancellationToken);
        return RedirectPermanent(result);
    }
}