using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Eventing.ApiService.Services;

public class JwtTokenService
{
    private readonly IConfiguration _config;
    
    public JwtTokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateJwtToken(Guid UserID, string username)
    {
        var jwtSettings = _config.GetSection("JwtSettings");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, UserID.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["TokenValidityMins"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}