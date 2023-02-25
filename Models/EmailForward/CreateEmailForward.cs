using System.Text.Json.Serialization;

namespace RedirectPizza.NET.Models.EmailForward;

public class CreateEmailForward
{
    /// <summary>
    /// The alias path (i.e. the part before the '@'). Using a '*' character will create a catch-all email forward.
    /// </summary>
    [JsonPropertyName("alias")]
    public string Alias { get; set; }

    /// <summary>
    /// Domain of the email forward for the 'from' part. Must be a domain you own and can manage DNS for.
    /// </summary>
    [JsonPropertyName("domain")]
    public string Domain { get; set; }

    /// <summary>
    /// Where to forward the email to. Must be a valid email address.
    /// </summary>
    [JsonPropertyName("destination")]
    public string Destination { get; set; }
}

internal class UpdateEmailForward
{
    /// <summary>
    /// The alias path (i.e. the part before the '@'). Using a '*' character will create a catch-all email forward.
    /// </summary>
    [JsonPropertyName("alias")]
    public string? Alias { get; set; }

    /// <summary>
    /// Domain of the email forward for the 'from' part. Must be a domain you own and can manage DNS for.
    /// </summary>
    [JsonPropertyName("domain")]
    public Domain.Domain? Domain { get; set; }

    /// <summary>
    /// Where to forward the email to. Must be a valid email address.
    /// </summary>
    [JsonPropertyName("destination")]
    public string? Destination { get; set; }
}