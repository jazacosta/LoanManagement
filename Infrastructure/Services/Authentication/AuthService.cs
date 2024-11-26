using Core.Interfaces.Services;
using Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Authentication;

public class AuthService : IAuthService
{
    public string CreateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var privateKey = Encoding.UTF8.GetBytes(JwtConfig.Secret);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = credentials,
            Subject = GenerateClaims(user)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static ClaimsIdentity GenerateClaims(User user)
    {
        var claims = new ClaimsIdentity();

        claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

        foreach (var rol in user.Roles)
        {
            claims.AddClaim(new Claim(ClaimTypes.Role, rol));
        }

        return claims;
    }

    public bool ValidateJwt(string token)
    {
        var privateKey = Encoding.UTF8.GetBytes(JwtConfig.Secret);

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(privateKey)
            };

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validParameters, out SecurityToken validatedToken);
            var usernameClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            return true;
        }
        catch
        {
            return false;
        }

    }
}