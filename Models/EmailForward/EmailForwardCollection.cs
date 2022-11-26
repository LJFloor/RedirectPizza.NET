using System.Text.Json.Serialization;
using RedirectPizza.NET.Models.General;

namespace RedirectPizza.NET.Models.EmailForward;

public class EmailForwardCollection
{
    [JsonPropertyName("data")]
    public List<EmailForward> Data { get; set; }

    [JsonPropertyName("meta")]
    public Meta Meta { get; set; }
}