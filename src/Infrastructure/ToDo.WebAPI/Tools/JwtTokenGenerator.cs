using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using ToDo.WebAPI.Authentication;

namespace ToDo.WebAPI.Tools;

public class JwtTokenGenerator
{
    public static string GenerateToken(string subject, TimeSpan expires)
    {
        var claims = new List<Claim> { new(ClaimTypes.Name, subject) };
        var jwt = new JwtSecurityToken(
            AuthOptions.ISSUER,
            AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(expires),
            signingCredentials: new SigningCredentials(
                AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}