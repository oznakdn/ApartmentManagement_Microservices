using System.Net.Http.Headers;

namespace Client.WebAdmin.ClientServices;

public class ClientServiceBase
{
    protected readonly HttpClient _httpClient;
    protected readonly IHttpContextAccessor _contextAccessor;
    public ClientServiceBase(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor)
    {
        _httpClient = clientFactory.CreateClient();
        _contextAccessor = contextAccessor;
    }

    public virtual void AddAuthorizationHeader()
    {
        var token = _contextAccessor.HttpContext!.Request.Cookies["access_token"];
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

}
