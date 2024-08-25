namespace Report.GraphQL.Services;

public abstract class ServiceBase
{
    public HttpClient _httpClient { get; protected set; }
    public void AddAuthorizationHeader(string token)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }
}
