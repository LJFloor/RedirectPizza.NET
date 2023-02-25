using RedirectPizza.NET.Models.General;
using RedirectPizza.NET.Models.Team;
using RestSharp;

namespace RedirectPizza.NET.Endpoints;

public class TeamEndpoint : Endpoint
{
    internal TeamEndpoint(RestClient client) : base(client) { }

    /// <summary>
    /// Get the team you are in.
    /// </summary>
    /// <returns>The Team object</returns>
    public async Task<Team> GetTeamAsync() => (await Get<RedirectPizzaResource<Team>>("team")).Data;

    public void GetTeam() => GetTeamAsync().GetAwaiter().GetResult();
}