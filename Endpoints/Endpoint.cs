using System.Net;
using System.Text.Json;
using RedirectPizza.NET.Exceptions;
using RedirectPizza.NET.Models.Redirect;
using RestSharp;

namespace RedirectPizza.NET.Endpoints;

public class Endpoint
{
    private const string BaseUrl = "https://redirect.pizza/api/v1/";
    
    /// Minimum time between requests.
    private const int Timeout = 50;
    
    private readonly RestClient _client;
    private DateTime? _lastRequest; 
    
    internal Endpoint(RestClient client) => _client = client;

    internal Task<T> Get<T>(string endpointUri) => Execute<T>(endpointUri, Method.Get);
    internal Task<T> Post<T>(string endpointUri, object? body = null) => Execute<T>(endpointUri, Method.Post, body);
    internal Task Post(string endpointUri, object? body = null) => Execute(endpointUri, Method.Post, body);
    internal Task<T> Put<T>(string endpointUri, object? body = null) => Execute<T>(endpointUri, Method.Put, body);
    internal Task Put(string endpointUri, object? body = null) => Execute(endpointUri, Method.Put, body);
    internal Task Delete(string endpointUri) => Execute(endpointUri, Method.Delete);

    private async Task<string?> Execute(string endpointUri, Method httpMethod, object? obj = null)
    {
        var request = new RestRequest(BaseUrl + endpointUri, httpMethod);
        if (obj != null)
        {
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(obj);
        }
        await WaitForTimeout();
        
        var response = await _client.ExecuteAsync(request);
        _lastRequest = DateTime.Now;
        
        if (response.IsSuccessful) return response.Content;

        throw MapResponseToException(response);
    }

    private async Task<T> Execute<T>(string endpointUri, Method httpMethod, object? obj = null) =>
        JsonSerializer.Deserialize<T>((await Execute(endpointUri, httpMethod, obj))!)!;

    private async Task WaitForTimeout()
    {
        if (_lastRequest != null)
        {
            var difference = (DateTime.Now - _lastRequest).Value.Milliseconds;
            if (difference < Timeout) await Task.Delay(Timeout - difference);
        }
    }

    private static Exception MapResponseToException(RestResponseBase response)
    {
        if (response.Content == null) 
            return new Exception($"{(int)response.StatusCode} {response.StatusDescription}");
        
        var error = JsonSerializer.Deserialize<RedirectPizzaError>(response.Content)!;
        return response.StatusCode switch
        {
            HttpStatusCode.UnprocessableEntity => new UnprocessableEntityException(error.Message),
            HttpStatusCode.NotFound => new ResourceNotFoundException(error.Message),
            HttpStatusCode.PaymentRequired => new PlanLimitReachedException(error.Message),
            _ => new Exception($"{(int)response.StatusCode} {response.StatusDescription}")
        };
    }
}