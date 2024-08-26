namespace Report.GraphQL.Services;

public abstract class ServiceBase
{
    public HttpClient _httpClient { get; protected set; }
    public void AddAuthorizationHeader(string token)
    {

        if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        else
        {
            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        }
        
        //_httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }
}
