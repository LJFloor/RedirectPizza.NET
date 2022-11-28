using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using RedirectPizza.NET.Endpoints;
using RedirectPizza.NET.Models.General;

namespace RedirectPizza.NET.Models.Redirect;

public class Redirect
{
    private RedirectEndpoint? _endpoint;

    internal Redirect WithEndpoint(RedirectEndpoint endpoint)
    {
        _endpoint = endpoint;
        return this;
    }

    /// <summary>
    /// Confirm all changes made to the model
    /// </summary>
    public async Task SaveAsync() =>
        await _endpoint.UpdateFromModelAsync(this);
    
    /// <summary>
    /// Confirm all changes made to the model
    /// </summary>
    public void Save() => SaveAsync().GetAwaiter().GetResult();

    /// <summary>
    /// Delete the redirect
    /// </summary>
    public async Task DeleteAsync() => await _endpoint.DeleteFromModelAsync(Id);
    
    /// <summary>
    /// Delete the redirect
    /// </summary>
    public void Delete() => DeleteAsync().GetAwaiter().GetResult();
    
    /// <summary>
    /// Id of the redirect
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// URLs to be redirected, i.e. the 'from' URLs
    /// </summary>
    [JsonPropertyName("sources")]
    public List<Source> Sources { get; set; }

    /// <summary>
    /// Parsed domains from the defined sources
    /// </summary>
    [JsonPropertyName("domains")]
    public IReadOnlyList<Domain> Domains { get; init; }

    /// <summary>
    /// Destination URL, i.e. where to redirect to
    /// </summary>
    [JsonPropertyName("destination")]
    public string Destination { get; set; }

    /// <summary>
    /// The type of redirect to use
    /// </summary>
    [JsonPropertyName("redirect_type")]
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public RedirectType RedirectType { get; set; } = RedirectType.Permanent;

    /// <summary>
    /// Whether the query string should be forwarded to the destination URL
    /// </summary>
    [JsonPropertyName("keep_query_string")]
    public bool KeepQueryString { get; set; } = false;

    /// <summary>
    /// Whether the path should be forwarded to the destination
    /// </summary>
    [JsonPropertyName("uri_forwarding")]
    public bool UriForwarding { get; set; } = false;

    /// <summary>
    /// Whether analytical information should be collected
    /// </summary>
    [JsonPropertyName("tracking")] 
    public bool Tracking { get; set; } = true;

    /// <summary>
    /// Used to categorize redirects
    /// </summary>
    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; }

    [JsonPropertyName("created_at")]
    public DateTime? CreatedAt { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime? UpdatedAt { get; init; }
}

public class Source
{
    public Source(string url) => Url = url;
    internal Source() {}
    
    /// <summary>
    /// The id of the source
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// The source URL including path definitions
    /// </summary>
    [JsonPropertyName("url")]
    public string Url { get; set; }
}

public enum RedirectType
{
    [EnumMember(Value = "permanent")]
    Permanent,
    [EnumMember(Value = "temporary")]
    Temporary,
    [EnumMember(Value = "frame")]
    Frame,
    [EnumMember(Value = "permanent:308")]
    Permanent308,
    [EnumMember(Value = "temporary:307")]
    Temporary307
}