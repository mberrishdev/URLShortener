using URLShortener.Domain.Entities.Urls.Commands;
using URLShortener.Domain.Primitives;

namespace URLShortener.Domain.Entities.Urls;

public class Url : Entity<long>
{
    public string Original { get; private set; }
    public string Shortened { get; private set; }
    public string Code { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Url()
    {
    }

    public Url(CreateUrlCommand command)
    {
        command.Validate();

        Original = command.Original;
        Shortened = command.Shortened!;
        Code = command.Code!;

        CreatedAt = DateTime.Now;
    }
}