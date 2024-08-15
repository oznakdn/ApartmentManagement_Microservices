using Client.WebAdmin.Constants;
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


        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, response.Email),
            new Claim(ClaimTypes.NameIdentifier, response.Id),
        };

        if (!string.IsNullOrWhiteSpace(response.Role))
            claims.Add(new Claim(ClaimTypes.Role, response.Role!));

        if (!string.IsNullOrWhiteSpace(response.SiteId))
            claims.Add(new Claim("SiteId", response.SiteId!));

        if (!string.IsNullOrWhiteSpace(response.UnitId))
            claims.Add(new Claim("UnitId", response.UnitId!));

        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "AuthScheme");

        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


        _contextAccessor.HttpContext!.Response.Cookies.Append(CookieConst.ACCESS_TOKEN, response.AccessToken, new CookieOptions
        {
            HttpOnly = false,
            Expires = response.AccessExpire
        });

        _contextAccessor.HttpContext.Response.Cookies.Append(CookieConst.REFRESH_TOKEN, response.RefreshToken, new CookieOptions
        {
            HttpOnly = false,
            Expires = response.RefreshExpire
        });

        await _contextAccessor.HttpContext!.SignInAsync("AuthScheme", claimsPrincipal);

    }
}
