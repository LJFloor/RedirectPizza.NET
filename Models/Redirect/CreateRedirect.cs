using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace RedirectPizza.NET.Models.Redirect;

public class CreateRedirect
{
    /// <summary>
    /// URLs to be redirected, i.e. the 'from' URLs.
    /// </summary>
    [JsonPropertyName("sources")]
    public IEnumerable<string> Sources { get; set; }
    
    /// <summary>
    /// Destination URL, i.e. where to redirect to.
    /// </summary>
    [JsonPropertyName("destination")]
    public string Destination { get; set; }
    
    /// <summary>
    /// The type of redirect to use.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    [JsonPropertyName("redirect_type")]
    public RedirectType RedirectType { get; set; } = RedirectType.Permanent;
    
    /// <summary>
    /// Whether the path should be forwarded to the destination.
    /// </summary>
    [JsonPropertyName("uri_forwarding")]
    public bool UriForwarding { get; set; } = false;
    
    /// <summary>
    /// Whether the query string should be forwarded to the destination URL.
    /// </summary>
    [JsonPropertyName("keep_query_string")]
    public bool KeepQueryString { get; set; } = false;
    
    /// <summary>
    /// Whether analytical information should be collected.
    /// </summary>
    [JsonPropertyName("tracking")]
    public bool Tracking { get; set; } = true;
    
    /// <summary>
    /// Used to categorize redirects.
    /// </summary>
    [JsonPropertyName("tags")]
    public IEnumerable<string>? Tags
    {
        get
        {
            if (_tags != null) 
                return _tags.Select(t => Regex.Replace(t, "[^a-zA-Z0-9]", "_"));
            return null;
        }
        set { _tags = value; }
    }

    private IEnumerable<string>? _tags { get; set; }
}

internal class UpdateRedirect
{
    [JsonPropertyName("sources")]
    public IEnumerable<string>? Sources { get; set; }
    
    [JsonPropertyName("destination")]
    public string? Destination { get; set; }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    [JsonPropertyName("redirect_type")]
    public RedirectType? RedirectType { get; set; }

    [JsonPropertyName("uri_forwarding")]
    public bool? UriForwarding { get; set; }
    
    [JsonPropertyName("keep_query_string")]
    public bool? KeepQueryString { get; set; }
    
    [JsonPropertyName("tracking")]
    public bool? Tracking { get; set; }

    [JsonPropertyName("tags")]
    public IEnumerable<string>? Tags
    {
        get
        {
            if (_tags != null) 
                return _tags.Select(t => Regex.Replace(t, "[^a-zA-Z0-9]", "_"));
            return null;
        }
        set { _tags = value; }
    }

    private IEnumerable<string>? _tags { get; set; }
}