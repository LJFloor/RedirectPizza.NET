using System.Text.Json.Serialization;

namespace RedirectPizza.NET.Models.General;

public class RedirectPizzaCollection<T> 
{
    /// <summary>
    /// The items in the collection.
    /// </summary>
    [JsonPropertyName("data")]
    public List<T> Items { get; init; }

    /// <summary>
    /// Meta information about the collection.
    /// </summary>
    [JsonPropertyName("meta")]
    public Meta Meta { get; init; }
}