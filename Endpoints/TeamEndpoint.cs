using RedirectPizza.NET.Models.General;
using RedirectPizza.NET.Models.Team;
using RestSharp;

namespace RedirectPizza.NET.Endpoints;

public class TeamEndpoint : Endpoint
{
    internal TeamEndpoint(RestClient client) : base(client) { }

    public async Task<Team> GetTeamAsync() => (await Get<RedirectPizzaResource<Team>>("team")).Data;
}