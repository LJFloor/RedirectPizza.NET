using System.Text.Json.Serialization;

namespace RedirectPizza.NET.Models.General;

public class Meta
{
    /// <summary>
    /// Current pagination number.
    /// </summary>
    [JsonPropertyName("current_page")]
    public int CurrentPage { get; init; }

    /// <summary>
    /// From pagination number.
    /// </summary>
    [JsonPropertyName("from")]
    public int? From { get; init; }

    /// <summary>
    /// Last pagination number.
    /// </summary>
    [JsonPropertyName("last_page")]
    public int LastPage { get; init; }

    /// <summary>
    /// Base URL.
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; init; }

    /// <summary>
    /// Number of items per page.
    /// </summary>
    [JsonPropertyName("per_page")]
    public int PerPage { get; init; }

    /// <summary>
    /// To pagination number.
    /// </summary>
    [JsonPropertyName("to")]
    public int? To { get; init; }

    /// <summary>
    /// Total number of items.
    /// </summary>
    [JsonPropertyName("total")]
    public int Total { get; init; }
}