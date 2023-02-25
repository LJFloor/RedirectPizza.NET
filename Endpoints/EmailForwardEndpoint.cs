using RedirectPizza.NET.Exceptions;
using RedirectPizza.NET.Models.EmailForward;
using RedirectPizza.NET.Models.General;
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
    /// <returns>RedirectPizzaCollection</returns>
    public async Task<RedirectPizzaCollection<EmailForward>> ListEmailForwardsAsync(int page = 1, int perPage = int.MaxValue)
    {
        var emailForwards = await Get<RedirectPizzaCollection<EmailForward>>($"email-forwards?page={page}&per_page={perPage}");
        emailForwards.Items.ForEach(s => s.WithEndpoint(this));
        return emailForwards;
    }

    /// <summary>
    /// Gets a list of email forwards
    /// </summary>
    /// <param name="page">The page to show</param>
    /// <param name="perPage">The amount of items to show per page. Default is all.</param>
    /// <returns>RedirectPizzaCollection</returns>
    public RedirectPizzaCollection<EmailForward> ListEmailForwards(int page = 1, int perPage = int.MaxValue) =>
        ListEmailForwardsAsync(page, perPage).GetAwaiter().GetResult();

    /// <summary>
    /// Gets a email forward by its id. Returns null if not found.
    /// </summary>
    /// <param name="id">The id of the email forward</param>
    /// <returns>EmailForward || null</returns>
    public async Task<EmailForward?> GetEmailForwardAsync(int id)
    {
        try
        {
            return (await Get<RedirectPizzaResource<EmailForward>>($"email-forwards/{id}")).Data
                .WithEndpoint(this);
        }
        catch (ResourceNotFoundException)
        {
            return null;
        }
    }
    
    /// <summary>
    /// Gets a email forward by its id. Returns null if not found.
    /// </summary>
    /// <param name="id">The id of the email forward</param>
    /// <returns>EmailForward || null</returns>
    public EmailForward? GetEmailForward(int id) => GetEmailForwardAsync(id).GetAwaiter().GetResult();

    /// <summary>
    /// Create a new email forward
    /// </summary>
    /// <param name="createEmailForward">
    /// The CreateEmailForward model containing all information about the email forward
    /// </param>
    /// <returns>The newly created EmailForward object</returns>
    public async Task<EmailForward> CreateEmailForwardAsync(CreateEmailForward createEmailForward) =>
        (await Post<RedirectPizzaResource<EmailForward>>("email-forwards", createEmailForward)).Data
            .WithEndpoint(this);

    /// <summary>
    /// Create a new email forward
    /// </summary>
    /// <param name="createEmailForward">
    /// The CreateEmailForward model containing all information about the email forward
    /// </param>
    /// <returns>The newly created EmailForward object</returns>
    public EmailForward CreateEmailForward(CreateEmailForward createEmailForward) =>
        CreateEmailForwardAsync(createEmailForward).GetAwaiter().GetResult();
    
    internal async Task UpdateFromModelAsync(EmailForward emailForward)
    {
        var updateEmailForward = new UpdateEmailForward()
        {
            Domain = emailForward.Domain,
            Alias = emailForward.Alias,
            Destination = emailForward.Destination
        };
        
        await Put($"email-forwards/{emailForward.Id}", updateEmailForward);
    }
    
    /// <summary>
    /// Delete an email forward by its id
    /// </summary>
    /// <param name="id">The id of the email forward that has to be deleted</param>
    /// <returns></returns>
    internal Task DeleteFromModelAsync(int id) => Delete($"email-forwards/{id}");
}