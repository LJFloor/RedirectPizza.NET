using System.Globalization;
using RedirectPizza.NET.Exceptions;
using RedirectPizza.NET.Models;
using RedirectPizza.NET.Models.Redirect;
using RestSharp;

namespace RedirectPizza.NET.Endpoints;

public class RedirectEndpoint : Endpoint
{
    internal RedirectEndpoint(RestClient client) : base(client) { }

    /// <summary>
    /// Gets a list of redirects
    /// </summary>
    /// <param name="page">The page to show</param>
    /// <param name="perPage">The amount of items to show per page. Default is all.</param>
    /// <returns>RedirectCollection</returns>
    public async Task<RedirectCollection> ListRedirectsAsync(int page = 1, int perPage = int.MaxValue)
    {
        var redirections = await Get<RedirectCollection>($"redirects?page={page}&per_page={perPage}");
        redirections.Data.ForEach(s => s.WithEndpoint(this));
        return redirections;
    }

    /// <summary>
    /// Get a list of redirects
    /// </summary>
    /// <param name="page">The page to show</param>
    /// <param name="perPage">The amount of items to show per page. Default is all.</param>
    /// <returns>RedirectCollection</returns>
    public RedirectCollection ListRedirects(int page = 1, int perPage = int.MaxValue) =>
        ListRedirectsAsync(page, perPage).GetAwaiter().GetResult();

    /// <summary>
    /// Gets a redirect by its id. Returns null if not found.
    /// </summary>
    /// <param name="id">The id of the redirect to get</param>
    /// <returns>Redirect || null</returns>
    public async Task<Redirect?> GetRedirectAsync(int id)
    {
        try
        {
            return (await Get<GetRedirectResponse>($"redirects/{id}")).Data.WithEndpoint(this);
        }
        catch (ResourceNotFoundException)
        {
            return null;
        }
    }

    /// <summary>
    /// Gets a redirect by its id. Returns null if not found.
    /// </summary>
    /// <param name="id">The id of the redirect to get</param>
    /// <returns>Redirect || null</returns>
    public Redirect? GetRedirect(int id) => GetRedirectAsync(id).GetAwaiter().GetResult();

    /// <summary>
    /// Create a new redirect
    /// </summary>
    /// <param name="createRedirect">The CreateRedirect object to add</param>
    /// <returns>The newly created redirect object</returns>
    public async Task<Redirect> CreateRedirectAsync(CreateRedirect createRedirect) => 
        (await Post<GetRedirectResponse>("redirects", createRedirect)).Data.WithEndpoint(this);

    /// <summary>
    /// Create a new redirect
    /// </summary>
    /// <param name="createRedirect">The CreateRedirect object to add</param>
    /// <returns>The newly created redirect object</returns>
    public Redirect CreateRedirect(CreateRedirect createRedirect) =>
        CreateRedirectAsync(createRedirect).GetAwaiter().GetResult();
    
    /// <summary>
    /// Update a redirect. All fields in the UpdateRedirect object are optional (nullable)
    /// </summary>
    /// <param name="id">The id of the redirect to update</param>
    /// <param name="updateRedirect">An UpdateRedirect object containing all fields that should be updated.</param>
    /// <returns>The updated redirect</returns>
    public async Task<Redirect?> UpdateRedirectAsync(int id, UpdateRedirect updateRedirect)
    {
        var result = await Put<Redirect?>($"redirects/{id}", updateRedirect);
        return result?.WithEndpoint(this);
    }

    public Redirect? UpdateRedirect(int id, UpdateRedirect updateRedirect) =>
        UpdateRedirectAsync(id, updateRedirect).GetAwaiter().GetResult();

    internal async Task<Redirect?> UpdateFromModelAsync(Redirect redirect)
    {
        var updateRedirect = new UpdateRedirect
        {
            Sources = redirect.Sources.Select(r => r.Url),
            Destination = redirect.Destination,
            RedirectType = redirect.RedirectType,
            UriForwarding = redirect.UriForwarding,
            KeepQueryString = redirect.KeepQueryString,
            Tracking = redirect.Tracking,
            Tags = redirect.Tags
        };
        
        var result = await Put<Redirect?>($"redirects/{redirect.Id}", updateRedirect);
        return result?.WithEndpoint(this);
    }
    

    /// <summary>
    /// Delete a redirect by its id.
    /// </summary>
    /// <param name="id">The id of the redirect to delete</param>
    public async Task DeleteRedirectAsync(int id) => await Delete($"redirects/{id}");
}