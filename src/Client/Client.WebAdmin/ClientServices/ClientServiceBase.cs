using Client.WebAdmin.Constants;
using Microsoft.AspNetCore.Authentication;
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

    public virtual async Task AddAuthorizationHeader()
    {
        var token = _contextAccessor.HttpContext!.Request.Cookies[CookieConst.ACCESS_TOKEN];

        if (string.IsNullOrWhiteSpace(token))
        {
            await _contextAccessor.HttpContext.SignOutAsync();
            _contextAccessor.HttpContext.Response.Redirect("/account/login");
            return;
        }

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
    }

}
