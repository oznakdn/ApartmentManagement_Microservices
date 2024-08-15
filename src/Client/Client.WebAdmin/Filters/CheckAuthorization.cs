﻿using Client.WebAdmin.Constants;
using Client.WebAdmin.Models.AccountModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Client.WebAdmin.Filters;

public class CheckAuthorization : ActionFilterAttribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

        string? accessToken = context.HttpContext.Request.Cookies[CookieConst.ACCESS_TOKEN];

        if (string.IsNullOrEmpty(accessToken))
        {

            string? refreshToken = context.HttpContext.Request.Cookies[CookieConst.REFRESH_TOKEN];

            if (!string.IsNullOrEmpty(refreshToken))
            {
                var clientService = context.HttpContext.RequestServices.GetRequiredService<HttpClient>();

                string url = $"{Endpoints.Account.RefreshLogin}/{refreshToken}";

                var responseMessage = await clientService.GetAsync(url);


                if (!responseMessage.IsSuccessStatusCode)
                {
                    await context.HttpContext.SignOutAsync("AuthScheme");
                    context.Result = new RedirectToPageResult("/Account/Login");

                }
                else
                {
                    var response = await responseMessage.Content.ReadFromJsonAsync<LoginResponse>();
                   
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, response.Email),
                        new Claim(ClaimTypes.NameIdentifier, response.Id)
                    };

                    if (!string.IsNullOrWhiteSpace(response.Role))
                        claims.Add(new Claim(ClaimTypes.Role, response.Role!));

                    if (!string.IsNullOrWhiteSpace(response.SiteId))
                        claims.Add(new Claim("SiteId", response.SiteId!));

                    if (!string.IsNullOrWhiteSpace(response.UnitId))
                        claims.Add(new Claim("UnitId", response.UnitId!));


                    var claimsIdentity = new ClaimsIdentity(claims, "AuthScheme");
                    var claimPrinciple = new ClaimsPrincipal(claimsIdentity);


                    context.HttpContext.Response.Cookies.Append(CookieConst.ACCESS_TOKEN, response.AccessToken, new CookieOptions
                    {
                        HttpOnly = false,
                        Expires = response.AccessExpire
                    });

                    context.HttpContext.Response.Cookies.Append(CookieConst.REFRESH_TOKEN, response.RefreshToken, new CookieOptions
                    {
                        HttpOnly = false,
                        Expires = response.RefreshExpire
                    });

                    await context.HttpContext!.SignInAsync("AuthScheme", claimPrinciple);

                    context.Result = new RedirectToPageResult("/Index");

                }

            }
            else
            {

                context.Result = new RedirectToPageResult("/Account/Login");
            }

        }

    }
}
