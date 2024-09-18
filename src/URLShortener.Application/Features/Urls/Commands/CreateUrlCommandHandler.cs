using Common.Repository.Repository;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using URLShortener.Application.Exceptions;
using URLShortener.Domain.Entities.Urls;
using URLShortener.Domain.Entities.Urls.Commands;

namespace URLShortener.Application.Features.Urls.Commands;

public class CreateUrlCommandHandler
    (IRepository<Url> repository, IDistributedCache distributedCache) : IRequestHandler<CreateUrlCommand, string>
{
    public async Task<string> Handle(CreateUrlCommand command, CancellationToken cancellationToken)
    {
        var tmp = await repository.GetAsync(x => x.Original.Equals(command.Original),
            cancellationToken: cancellationToken);

        if (tmp is not null)
        {
            await Cache(tmp.Code, command.Original, cancellationToken);
            return tmp.Shortened;
        }

        var code = await GenerateCode(cancellationToken);

        command.Shortened = $"{command.Schema}://{command.Host}/v1/url/{code}";
        command.Code = code;
        var url = new Url(command);

        await repository.InsertAsync(url, cancellationToken);

        await Cache(code, command.Original, cancellationToken);

        return url.Shortened;
    }

    private async Task<string> GenerateCode(CancellationToken cancellationToken)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();

        while (true)
        {
            var code = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            if (!await repository.ExistsAsync(x => x.Code == code, cancellationToken: cancellationToken))
                return code;
        }
    }

    private async Task Cache(string code, string original, CancellationToken cancellationToken)
    {
        try
        {
            await distributedCache.SetStringAsync(code, original, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
            }, cancellationToken);
        }
        catch (Exception e)
        {
            // ignored
        }
    }
}