using Account.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Shared.Core.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Account.Application.Helpers.Token;

public class JwtTokenHelper(JwtOption jwtOption, UserManager<User> userManager)
{
    public async Task<TokenResponse> GenerateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOption.SecretKey));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!)
        };

        if (await userManager.IsInRoleAsync(user, RoleConstant.ADMIN))
        {
            claims.Add(new Claim(ClaimTypes.Role, RoleConstant.ADMIN));
        }

        if (await userManager.IsInRoleAsync(user, RoleConstant.MANAGER))
        {
            claims.Add(new Claim(ClaimTypes.Role, RoleConstant.MANAGER));
        }

        if (await userManager.IsInRoleAsync(user, RoleConstant.RESIDENT))
        {
            claims.Add(new Claim(ClaimTypes.Role, RoleConstant.RESIDENT));
        }

        if (await userManager.IsInRoleAsync(user, RoleConstant.GUARD))
        {
            claims.Add(new Claim(ClaimTypes.Role, RoleConstant.GUARD));
        }

        int expire = jwtOption.ExpiryMinutes;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = jwtOption.Issuer,
            Audience = jwtOption.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(expire),
            SigningCredentials = credentials
        };

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        return new TokenResponse
        {
            AccessToken = accessToken,
            AccessExpIn = tokenDescriptor.Expires!.Value,
            RefreshToken = GenerateRefreshToken(),
            RefreshExpIn = tokenDescriptor.Expires.Value.AddMinutes(15)
        };
    }

    private string GenerateRefreshToken() => Ulid.NewUlid().ToString();

}
