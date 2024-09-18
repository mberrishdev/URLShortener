using Common.Repository.Repository;
using MediatR;
using URLShortener.Application.Exceptions;
using URLShortener.Domain.Entities.Urls;

namespace URLShortener.Application.Features.Urls.Queries;

public sealed record GetUrlByCodeQuery(string Code) : IRequest<string>;

public class GetUrlByCodeQueryHandler
    (IQueryRepository<Url> repository) : IRequestHandler<GetUrlByCodeQuery, string>
{
    public async Task<string> Handle(GetUrlByCodeQuery request, CancellationToken cancellationToken)
    {
        var url = await repository.GetAsync(predicate: x => x.Code == request.Code,
                      cancellationToken: cancellationToken)
                  ?? throw new ObjectNotFoundException("URL Not found");

        return url.Original;
    }
}