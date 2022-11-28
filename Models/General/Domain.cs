using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RedirectPizza.NET.Models.General;

public class Domain
{
    /// <summary>
    /// The id of the domain.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// The FQDN of the domain.
    /// </summary>
    [JsonPropertyName("fqdn")]
    public string Fqdn { get; init; }

    /// <summary>
    /// Whether the domain is a root domain.
    /// </summary>
    [JsonPropertyName("is_root_domain")]
    public bool IsRootDomain { get; init; }

    /// <summary>
    /// Whether a HSTS header should be set for requests to this domain.
    /// </summary>
    [JsonPropertyName("hsts")]
    public bool Hsts { get; init; }

    /// <summary>
    /// Whether a 'prevent foreign embedding' header should be set for requests to this domain.
    /// </summary>
    [JsonPropertyName("prevent_foreign_embedding")]
    public bool PreventForeignEmbedding { get; init; }

    /// <summary>
    /// The DNS settings.
    /// </summary>
    [JsonPropertyName("dns")]
    public Dns Dns { get; init; }

    /// <summary>
    /// SSL settings
    /// </summary>
    [JsonPropertyName("ssl")]
    public Ssl Ssl { get; init; }
    
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }
}

public class DnsRecord
{
    /// <summary>
    /// The type of record.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    [JsonPropertyName("type")]
    public DnsRecordType Type { get; init; }

    /// <summary>
    /// The required value. If multiple records are required (for MX), comma-separated values are used.
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; init; }
}

public class Ssl
{
    /// <summary>
    /// Whether SSL is activated.
    /// </summary>
    [JsonPropertyName("active")]
    public bool Active { get; init; }
}

public class Dns
{
    /// <summary>
    /// Whether the DNS settings have been verified.
    /// </summary>
    [JsonPropertyName("verified")]
    public bool Verified { get; init; }

    /// <summary>
    /// The required DNS settings.
    /// </summary>
    [JsonPropertyName("required_settings")]
    public List<DnsRecord> RequiredRecords { get; init; }
}


public enum DnsRecordType
{
    [EnumMember(Value = "A")]
    A,
    [EnumMember(Value = "CNAME")]
    Cname,
    [EnumMember(Value = "MX")]
    Mx,
    [EnumMember(Value = "TXT")]
    Txt
}