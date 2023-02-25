using RedirectPizza.NET.Exceptions;
using RedirectPizza.NET.Models.Domain;
using RedirectPizza.NET.Models.General;
using RestSharp;

namespace RedirectPizza.NET.Endpoints;

public class DomainEndpoint : Endpoint
{
    internal DomainEndpoint(RestClient client) : base(client) { }
    
    /// <summary>
    /// List all domains
    /// </summary>
    /// <param name="page">The page to show</param>
    /// <param name="perPage">The amount of items to show per page. Default is all.</param>
    /// <returns>RedirectPizzaCollection</returns>
    public async Task<RedirectPizzaCollection<Domain>> ListDomainsAsync(int page = 1, int perPage = int.MaxValue)
    {
        var domains = await Get<RedirectPizzaCollection<Domain>>($"domains?page={page}&per_page={perPage}");
        domains.Items.ForEach(s => s.WithEndpoint(this));
        return domains;
    }

    /// <summary>
    /// List all domains
    /// </summary>
    /// <param name="page">The page to show</param>
    /// <param name="perPage">The amount of items to show per page. Default is all.</param>
    /// <returns>RedirectPizzaCollection</returns>
    public RedirectPizzaCollection<Domain> ListDomains(int page = 1, int perPage = int.MaxValue) =>
        ListDomainsAsync(page, perPage).GetAwaiter().GetResult();

    /// <summary>
    /// Get a domain by it's id.
    /// </summary>
    /// <param name="id">The id of the domain to get</param>
    /// <returns>Domain || null</returns>
    public async Task<Domain?> GetDomainAsync(int id)
    {
        try
        {
            return (await Get<RedirectPizzaResource<Domain>>($"domains/{id}")).Data.WithEndpoint(this);
        }
        catch (ResourceNotFoundException)
        {
            return null;
        }
    }

    /// <summary>
    /// Get a domain by it's id.
    /// </summary>
    /// <param name="id">The id of the domain to get</param>
    /// <returns>Domain || null</returns>
    public Domain? GetDomain(int id) => GetDomainAsync(id).GetAwaiter().GetResult();

    internal async Task<Domain> CheckDns(int id) =>
        (await Post<RedirectPizzaResource<Domain>>($"domains/{id}/check-dns")).Data.WithEndpoint(this);
}