using Common.Repository.Repository;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using URLShortener.Application.Exceptions;
using URLShortener.Domain.Entities.Urls;

namespace URLShortener.Application.Features.Urls.Queries;

public sealed record GetUrlByCodeQuery(string Code) : IRequest<string>;

public class GetUrlByCodeQueryHandler
    (IQueryRepository<Url> repository, IDistributedCache distributedCache) : IRequestHandler<GetUrlByCodeQuery, string>
{
    public async Task<string> Handle(GetUrlByCodeQuery request, CancellationToken cancellationToken)
    {
        var cachedUrl = await distributedCache.GetStringAsync(request.Code, cancellationToken);
        if (cachedUrl != null) return cachedUrl;

        var url = await repository.GetAsync(x => x.Code == request.Code,
                      cancellationToken: cancellationToken)
                  ?? throw new ObjectNotFoundException("URL Not found");

        await distributedCache.SetStringAsync(request.Code, url.Original, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        }, cancellationToken);

        return url.Original;
    }
}