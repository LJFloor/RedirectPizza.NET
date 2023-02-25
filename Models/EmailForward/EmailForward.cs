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

    /// <summary>
    /// Confirm all changes made to the model.
    /// </summary>
    /// <returns></returns>
    public Task SaveAsync() => _endpoint.UpdateFromModelAsync(this);

    /// <summary>
    /// Confirm all changes made to the model.
    /// </summary>
    public void Save() => SaveAsync().GetAwaiter().GetResult();
    
    /// <summary>
    /// Delete the email forward.
    /// </summary>
    /// <returns></returns>
    public Task DeleteAsync() => _endpoint.DeleteFromModelAsync(Id);

    /// <summary>
    /// Delete the email forward.
    /// </summary>
    public void Delete() => DeleteAsync().GetAwaiter().GetResult();
    
    /// <summary>
    /// The id of the email forward.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }
    
    /// <summary>
    /// Information about the domain of the email forward.
    /// </summary>
    [JsonPropertyName("domain")] 
    public Domain.Domain? Domain { get; init; }

    /// <summary>
    /// The alias path (i.e. the part before the '@'). Using a '*' character will create a catch-all email forward.
    /// </summary>
    [JsonPropertyName("alias")]
    public string Alias { get; init; }

    /// <summary>
    /// Where to forward the email to. Must be a valid email address.
    /// </summary>
    [JsonPropertyName("destination")]
    public string Destination { get; set; }
}