using RedirectPizza.NET.Exceptions;
using RedirectPizza.NET.Models.EmailForward;
using RestSharp;

namespace RedirectPizza.NET.Endpoints;

public class EmailForwardEndpoint : Endpoint
{
    internal EmailForwardEndpoint(RestClient client) : base(client) { }

    /// <summary>
    /// Gets a list of email forwards
    /// </summary>
    /// <param name="page">The page to show</param>
    /// <param name="perPage">The amount of items to show per page. Default is all.</param>
    /// <returns>EmailForwardCollection</returns>
    public async Task<EmailForwardCollection> ListEmailForwardsAsync(int page = 1, int perPage = int.MaxValue)
    {
        var emailForwards = await Get<EmailForwardCollection>($"email-forwards?page={page}&per_page={perPage}");
        emailForwards.Data.ForEach(s => s.WithEndpoint(this));
        return emailForwards;
    }

    /// <summary>
    /// Gets a email forward by its id. Returns null if not found.
    /// </summary>
    /// <param name="id">The id of the email forward</param>
    /// <returns>EmailForward || null</returns>
    public async Task<EmailForward?> GetEmailForwardAsync(int id)
    {
        try
        {
            return (await Get<GetEmailForwardResponse>($"email-forwards/{id}")).Data.WithEndpoint(this);
        }
        catch (ResourceNotFoundException)
        {
            return null;
        }
    }

    /// <summary>
    /// Create a new email forward
    /// </summary>
    /// <param name="createEmailForward">
    /// The CreateEmailForward model containing all information about the email forward
    /// </param>
    /// <returns>The newly created EmailForward object</returns>
    public async Task<EmailForward> CreateEmailForwardAsync(CreateEmailForward createEmailForward) =>
        (await Post<GetEmailForwardResponse>("email-forwards", createEmailForward)).Data.WithEndpoint(this);

    /// <summary>
    /// Delete an email forward by its id
    /// </summary>
    /// <param name="id">The id of the email forward that has to be deleted</param>
    /// <returns></returns>
    public Task DeleteEmailForwardAsync(int id) => Delete($"email-forwards/{id}");
}