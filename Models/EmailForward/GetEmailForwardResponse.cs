using System.Text.Json.Serialization;

namespace RedirectPizza.NET.Models.EmailForward;

public class GetEmailForwardResponse
{
    [JsonPropertyName("data")]
    public EmailForward Data { get; set; }
}