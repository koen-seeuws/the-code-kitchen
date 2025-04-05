using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TheCodeKitchen.Application.Contracts.Interfaces.Authentication;
using TheCodeKitchen.Infrastructure.Security.Configuration;

namespace TheCodeKitchen.Infrastructure.Security;

public class JwtTokenService(JwtSecurityOptions jwtSecurityOptions) : ISecurityTokenService
{
    private readonly SymmetricSecurityKey _signingKey = new(Encoding.UTF8.GetBytes(jwtSecurityOptions.Secret));

    public string GeneratePlayerToken(string username, Guid kitchenId)
    {
        var signingCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim("Kitchen", kitchenId.ToString()),
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(jwtSecurityOptions.ValidForHours),
            SigningCredentials = signingCredentials,
            //Issuer = "your-issuer", // Optional: Set your issuer (identity provider)
            //Audience = "your-audience" // Optional: Set your audience (typically your API)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}