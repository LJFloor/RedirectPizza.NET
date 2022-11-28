using System.Text.Json.Serialization;

namespace RedirectPizza.NET.Models.General;

public class RedirectPizzaResource<T>
{
    /// <summary>
    /// The data of the resource.
    /// </summary>
    [JsonPropertyName("data")]
    public T Data { get; init; }
}