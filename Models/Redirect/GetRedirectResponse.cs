using System.Text.Json.Serialization;

namespace RedirectPizza.NET.Models.Redirect;

public class GetRedirectResponse
{
    [JsonPropertyName("data")]
    public Redirect Data { get; set; }
}