using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace RedirectPizza.NET.Models.Redirect;

public class CreateRedirect
{
    [JsonPropertyName("sources")]
    public IEnumerable<string> Sources { get; set; }
    
    [JsonPropertyName("destination")]
    public string Destination { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    [JsonPropertyName("redirect_type")]
    public RedirectType RedirectType { get; set; } = RedirectType.Permanent;
    
    [JsonPropertyName("uri_forwarding")]
    public bool UriForwarding { get; set; } = false;
    
    [JsonPropertyName("keep_query_string")]
    public bool KeepQueryString { get; set; } = false;
    
    [JsonPropertyName("tracking")]
    public bool Tracking { get; set; } = true;
    
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

public class UpdateRedirect
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