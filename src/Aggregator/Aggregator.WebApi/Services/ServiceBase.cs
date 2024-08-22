namespace Aggregator.WebApi.Services;

public abstract class ServiceBase
{
    protected readonly HttpClient _httpClient;
    protected ServiceBase(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public void AddAuthorizationHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }

}
