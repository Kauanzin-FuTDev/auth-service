using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Interfaces;
using Application.services;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Infraestructer.Services.AuthService;

public class TokenService : ITokenService
{
    public string GenerateToken(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(Configuration.PrivateKey);
        
        var credencials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credencials,
            Expires = DateTime.UtcNow.AddMinutes(30),
        };
        
        var token = handler.CreateToken(tokenDescriptor);
        
        return handler.WriteToken(token);
    }

    public ClaimsIdentity GenerateClaimsIdentity(User user)
    {
        var ci = new ClaimsIdentity();
        
        ci.AddClaim(new Claim(ClaimTypes.NameIdentifier.ToString(), user.Id.ToString()));
        foreach (var role in user.Roles)
        {
            ci.AddClaim(new Claim(ClaimTypes.Role, role.Name));
        }
        
        return ci;
    }
}