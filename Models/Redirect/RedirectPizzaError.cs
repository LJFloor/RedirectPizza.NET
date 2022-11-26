using System.Text.Json.Serialization;

namespace RedirectPizza.NET.Models.Redirect;

public class RedirectPizzaError
{
    [JsonPropertyName("message")]
    public string Message { get; set; } = "Could not even load the content of this exception.";
}