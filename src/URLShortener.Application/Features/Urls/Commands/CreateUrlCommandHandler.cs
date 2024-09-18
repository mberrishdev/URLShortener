using Common.Repository.Repository;
using MediatR;
using URLShortener.Application.Exceptions;
using URLShortener.Domain.Entities.Urls;
using URLShortener.Domain.Entities.Urls.Commands;

namespace URLShortener.Application.Features.Urls.Commands;

public class CreateUrlCommandHandler(IRepository<Url> repository) : IRequestHandler<CreateUrlCommand, string>
{
    public async Task<string> Handle(CreateUrlCommand command, CancellationToken cancellationToken)
    {
        var tmp = await repository.GetAsync(x => x.Original.Equals(command.Original),
            cancellationToken: cancellationToken);
        if (tmp is not null)
            throw new ObjectAlreadyExistException("URL already exist");

        var code = await GenerateCode(cancellationToken);

        command.Shortened = $"{command.Schema}://{command.Host}/v1/url/{code}";
        command.Code = code;
        var url = new Url(command);

        await repository.InsertAsync(url, cancellationToken);

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
}