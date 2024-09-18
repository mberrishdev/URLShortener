using System.Text.Json.Serialization;
using URLShortener.Domain.Primitives;

namespace URLShortener.Domain.Entities.Urls.Commands;

public class CreateUrlCommand : CommandBase<string>
{
    public required string Original { get; set; }
    [JsonIgnore] public string? Shortened { get; set; }
    [JsonIgnore] public string? Code { get; set; }
    [JsonIgnore] public string? Schema { get; set; }
    [JsonIgnore] public string? Host { get; set; }
    
}