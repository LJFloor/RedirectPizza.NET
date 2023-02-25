using RedirectPizza.NET.Endpoints;
using RestSharp;
using RestSharp.Authenticators.OAuth2;

namespace RedirectPizza.NET;

public class RedirectPizza
{
    private readonly RestClient _client;
    public RedirectPizza(string apiKey)
    {
        _client = new RestClient();
        _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(apiKey, "Bearer");
        _client.AddDefaultHeader("User-Agent", "RedirectPizza.NET/v1");
    }
    
    /// <summary>
    /// Source URLs are the incoming URLs that redirect to the destination URL.
    /// Destination is the URL where the visitor is sent, i.e. the target URL.
    /// </summary>
    public RedirectEndpoint Redirects => _redirects ??= new RedirectEndpoint(_client);
    private RedirectEndpoint? _redirects;

    /// <summary>
    /// Email forwards are used to forward emails to another email address. The incoming email addreses is seperated in
    /// 2 parts, the alias (before the '@') and the domain (after the '@'). The destination is the email address where
    /// the email is forwarded to.
    /// </summary>
    public EmailForwardEndpoint EmailForwards => _emailForwards ??= new EmailForwardEndpoint(_client);
    private EmailForwardEndpoint? _emailForwards;

    /// <summary>
    /// The team API allows you to fetch/analyse the team's quota.
    /// </summary>
    public TeamEndpoint Team => _team ??= new TeamEndpoint(_client);
    private TeamEndpoint? _team;

    public DomainEndpoint Domains => _domains ??= new DomainEndpoint(_client);
    private DomainEndpoint? _domains;
}