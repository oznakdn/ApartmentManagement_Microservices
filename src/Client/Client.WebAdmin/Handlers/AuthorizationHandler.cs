using Client.WebAdmin.Models.AccountModels;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Client.WebAdmin.Handlers;

public class AuthorizationHandler
{
    private readonly IHttpContextAccessor _contextAccessor;
    public AuthorizationHandler(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public async Task Authorize(LoginResponse response)
    {
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Email, response!.Email),
                new Claim(ClaimTypes.NameIdentifier, response.Id),
                new Claim(ClaimTypes.Role, response.Role ?? string.Empty)

            }, "AuthScheme");

        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


        _contextAccessor.HttpContext!.Response.Cookies.Append("access_token", response.AccessToken, new CookieOptions
        {
            HttpOnly = false,
            Expires = response.AccessExpire
        });

        _contextAccessor.HttpContext.Response.Cookies.Append("refresh_token", response.RefreshToken, new CookieOptions
        {
            HttpOnly = false,
            Expires = response.RefreshExpire
        });

        await _contextAccessor.HttpContext!.SignInAsync("AuthScheme", claimsPrincipal);

    }
}
