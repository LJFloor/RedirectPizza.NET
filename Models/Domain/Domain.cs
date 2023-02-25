using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using RedirectPizza.NET.Endpoints;
using RedirectPizza.NET.Models.General;

namespace RedirectPizza.NET.Models.Domain;

public class Domain
{
    private DomainEndpoint? _endpoint;
    
    internal Domain WithEndpoint(DomainEndpoint endpoint)
    {
        _endpoint = endpoint;
        return this;
    }
    
    /// <summary>
    /// The id of the domain.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// The FQDN of the domain.
    /// </summary>
    [JsonPropertyName("fqdn")]
    public string Fqdn { get; set; }

    /// <summary>
    /// Whether the domain is a root domain.
    /// </summary>
    [JsonPropertyName("is_root_domain")]
    public bool IsRootDomain { get; set; }

    /// <summary>
    /// Whether a HSTS header should be set for requests to this domain.
    /// </summary>
    [JsonPropertyName("hsts")]
    public bool Hsts { get; set; }

    /// <summary>
    /// Whether a 'prevent foreign embedding' header should be set for requests to this domain.
    /// </summary>
    [JsonPropertyName("prevent_foreign_embedding")]
    public bool PreventForeignEmbedding { get; set; }

    /// <summary>
    /// The DNS settings.
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

    /// <summary>
    /// Check the dns settings. Updates the properties of the current model.
    /// </summary>
    public async Task<Domain> CheckDnsAsync()
    {
        var updated = await _endpoint.CheckDns(Id);
        Id = updated.Id;
        Fqdn = updated.Fqdn;
        IsRootDomain = updated.IsRootDomain;
        Hsts = updated.Hsts;
        PreventForeignEmbedding = updated.PreventForeignEmbedding;
        Dns = updated.Dns;
        Ssl = updated.Ssl;
        CreatedAt = updated.CreatedAt;
        UpdatedAt = updated.UpdatedAt;
        return this;
    }
    
    /// <summary>
    /// Check the dns settings. Updates the properties of the current model.
    /// </summary>
    public Domain CheckDns() => CheckDnsAsync().GetAwaiter().GetResult();
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