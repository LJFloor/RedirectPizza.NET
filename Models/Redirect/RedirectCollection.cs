using System.Text.Json.Serialization;
using RedirectPizza.NET.Models.General;

namespace RedirectPizza.NET.Models.Redirect;

public class RedirectCollection
{
    [JsonPropertyName("data")]
    public List<Redirect> Data { get; set; }

    [JsonPropertyName("meta")]
    public Meta Meta { get; init; }
}