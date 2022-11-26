using System.Text.Json.Serialization;
using RedirectPizza.NET.Endpoints;
using RedirectPizza.NET.Models.General;

namespace RedirectPizza.NET.Models.EmailForward;

public class EmailForward
{
    private EmailForwardEndpoint? _endpoint;
    
    /// <summary>
    /// Adds the endpoint to the model, so things like email.Update() and email.Delete() work.
    /// </summary>
    /// <param name="endpoint"></param>
    /// <returns></returns>
    internal EmailForward WithEndpoint(EmailForwardEndpoint endpoint)
    {
        _endpoint = endpoint;
        return this;
    }
    
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("domain")] 
    public Domain Domain { get; set; }

    /// <summary>
    /// The alias path (i.e. the part before the '@'). Using a '*' character will create a catch-all email forward.
    /// </summary>
    [JsonPropertyName("alias")]
    public string Alias { get; set; }

    /// <summary>
    /// Where to forward the email to. Must be a valid email address.
    /// </summary>
    [JsonPropertyName("destination")]
    public string Destination { get; set; }
}