using System.Text.Json.Serialization;

namespace RedirectPizza.NET.Models.Team;

public class Team
{
    /// <summary>
    /// The id of the team.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }
    
    /// <summary>
    /// The The name of the team.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }
    
    /// <summary>
    /// The hostnames usage and quota.
    /// </summary>
    [JsonPropertyName("hostnames")]
    public HostnameStatistics Hostnames { get; init; }
    
    /// <summary>
    /// The hits usage and quota.
    /// </summary>
    [JsonPropertyName("hits")]
    public HitStatistics Hits { get; init; }
    
    /// <summary>
    /// The users usage and quota.
    /// </summary>
    [JsonPropertyName("users")]
    public UserStatistics Users { get; init; }
}

public class HostnameStatistics
{
    /// <summary>
    /// The current hostnames.
    /// </summary>
    [JsonPropertyName("current")]
    public int Current { get; init; }
    
    /// <summary>
    /// The max hostnames allowed in the current plan. -1 means unlimited.
    /// </summary>
    [JsonPropertyName("limit")]
    public int Limit { get; init; }
    
    /// <summary>
    /// Whether there is a limit.
    /// </summary>
    public bool HasLimit => Limit != -1;
}

public class HitStatistics
{
    /// <summary>
    /// The current hits in this month. Updated every minute.
    /// </summary>
    [JsonPropertyName("current")]
    public int? Current { get; init; }
    
    /// <summary>
    /// The projected hits in this month. Updated every minute.
    /// </summary>
    [JsonPropertyName("projected")]
    public int? Projected { get; init; }
    
    /// <summary>
    /// The max hits allowed in the current plan. -1 means unlimited.
    /// </summary>
    [JsonPropertyName("limit")]
    public int Limit { get; init; }
}

public class UserStatistics
{
    /// <summary>
    /// The current number of users under the team.
    /// </summary>
    [JsonPropertyName("current")]
    public int Current { get; init; }

    /// <summary>
    /// The max number of users allowed in the current plan. -1 if unlimited.
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }
    
    /// <summary>
    /// Whether there is a limit.
    /// </summary>
    public bool HasLimit => Limit != -1;
}