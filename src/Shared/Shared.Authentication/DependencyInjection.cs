using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shared.Authentication;

public static class DependencyInjection
{
    public static void AddJwtAuthentication(this IServiceCollection services, Action<JwtOption> options)
    {

        var jwtOption = new JwtOption();
        options.Invoke(jwtOption);

        services
            .AddAuthentication(scheme =>
            {
                scheme.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                scheme.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtOption.Issuer != null ? true : false,
                    ValidateAudience = jwtOption.Audience != null ? true : false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = jwtOption.SecretKey != null ? true : false,
                    ValidIssuer = jwtOption.Issuer ?? "",
                    ValidAudience = jwtOption.Audience ?? "",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey ?? ""))
                };
            });
    }
}
