using System.Globalization;
using RedirectPizza.NET.Exceptions;
using RedirectPizza.NET.Models;
using RedirectPizza.NET.Models.General;
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
    public async Task<RedirectPizzaCollection<Redirect>> ListRedirectsAsync(int page = 1, int perPage = int.MaxValue)
    {
        var redirections = await Get<RedirectPizzaCollection<Redirect>>($"redirects?page={page}&per_page={perPage}");
        redirections.Items.ForEach(s => s.WithEndpoint(this));
        return redirections;
    }

    /// <summary>
    /// Get a list of redirects
    /// </summary>
    /// <param name="page">The page to show</param>
    /// <param name="perPage">The amount of items to show per page. Default is all.</param>
    /// <returns>RedirectCollection</returns>
    public RedirectPizzaCollection<Redirect> ListRedirects(int page = 1, int perPage = int.MaxValue) =>
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
            return (await Get<RedirectPizzaResource<Redirect>>($"redirects/{id}")).Data.WithEndpoint(this);
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
        (await Post<RedirectPizzaResource<Redirect>>("redirects", createRedirect)).Data.WithEndpoint(this);

    //RedirectPizzaResource<Redirect>/ <summary>
    /// Create a new redRedirectPizzaResource<Redirect>irect
    /// </summary>
    /// <param name="createRedirect">The CreateRedirect object to add</param>
    /// <returns>The newly created redirect object</returns>
    public Redirect CreateRedirect(CreateRedirect createRedirect) =>
        CreateRedirectAsync(createRedirect).GetAwaiter().GetResult();

    /// <summary>
    /// Update all changes made to a model. This method is called by the Save() and SaveAsync() methods of a redirect
    /// object.
    /// </summary>
    /// <param name="redirect">The redirect to update.</param>
    internal async Task UpdateFromModelAsync(Redirect redirect)
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
        
        await Put($"redirects/{redirect.Id}", updateRedirect);
    }
    

    /// <summary>
    /// Delete a redirect by its id. This function is called by the Delete() and DeleteAsync() methods of a redirect
    /// object.
    /// </summary>
    /// <param name="id">The id of the redirect to delete</param>
    internal async Task DeleteFromModelAsync(int id) => await Delete($"redirects/{id}");
}