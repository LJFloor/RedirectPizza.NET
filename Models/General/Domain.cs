using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace RedirectPizza.NET.Models.General;

public class Domain
{
    /// <summary>
    /// The id of the domain
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// The FQDN of the domain
    /// </summary>
    [JsonPropertyName("fqdn")]
    public string Fqdn { get; set; }

    /// <summary>
    /// Whether the domain is a root domain.
    /// </summary>
    [JsonPropertyName("is_root_domain")]
    public bool IsRootDomain { get; set; }

    /// <summary>
    /// Whether a HSTS header should be set for requests to this domain
    /// </summary>
    [JsonPropertyName("hsts")]
    public bool Hsts { get; set; }

    /// <summary>
    /// Whether a 'prevent foreign embedding' header should be set for requests to this domain
    /// </summary>
    [JsonPropertyName("prevent_foreign_embedding")]
    public bool PreventForeignEmbedding { get; set; }

    /// <summary>
    /// The DNS settings
    /// </summary>
    [JsonPropertyName("dns")]
    public Dns Dns { get; set; }

    /// <summary>
    /// SSL settings
    /// </summary>
    [JsonPropertyName("ssl")]
    public Ssl Ssl { get; set; }
    
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; }
}

public class DnsRecord
{
    /// <summary>
    /// The type of record.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    [JsonPropertyName("type")]
    public DnsRecordType Type { get; set; }

    /// <summary>
    /// The required value. If multiple records are required (for MX), comma-separated values are used.
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class Ssl
{
    /// <summary>
    /// Whether SSL is activated.
    /// </summary>
    [JsonPropertyName("active")]
    public bool Active { get; set; }
}

public class Dns
{
    /// <summary>
    /// Whether the DNS settings have been verified
    /// </summary>
    [JsonPropertyName("verified")]
    public bool Verified { get; set; }

    /// <summary>
    /// The required DNS settings
    /// </summary>
    [JsonPropertyName("required_settings")]
    public List<DnsRecord> RequiredRecords { get; set; }
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