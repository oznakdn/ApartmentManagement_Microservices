using Client.WebAdmin.Constants;
using Client.WebAdmin.Handlers;
using Client.WebAdmin.Models.AccountModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Client.WebAdmin.Filters;

public class TokenCheckFilter : IAsyncPageFilter
{
    private readonly AuthorizationHandler _authorizationHandler;
    private readonly HttpClient _httpClient;
    public TokenCheckFilter(AuthorizationHandler authorizationHandler, IHttpClientFactory clientFactory)
    {
        _authorizationHandler = authorizationHandler;
        _httpClient = clientFactory.CreateClient();
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        var refreshToken = context.HttpContext.Request.Cookies[CookieConst.REFRESH_TOKEN];

        if(string.IsNullOrEmpty(refreshToken))
        {
            context.Result = new RedirectToPageResult("/Account/Login");
        }
        else
        {
            
            var accessToken = context.HttpContext.Request.Cookies[CookieConst.ACCESS_TOKEN];

            if (!string.IsNullOrEmpty(accessToken))
            {
                await next();
            }
            else
            {
                string url = $"{Endpoints.Account.RefreshLogin}";
                var httpResponse = await _httpClient.PutAsJsonAsync(url, new RefreshLoginRequest { RefreshToken = refreshToken });

                if(!httpResponse.IsSuccessStatusCode)
                {
                    context.Result = new RedirectToPageResult("/Account/Login");
                }
                else
                {
                    var response = await httpResponse.Content.ReadFromJsonAsync<LoginResponse>();
                    await _authorizationHandler.Authorize(response!);
                    var endpoint = context.HttpContext.Request.Path;
                    context.Result = new RedirectToPageResult(endpoint);
                    //await next();
                }
            }
        }

    }

    public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        await Task.CompletedTask;
    }
}
