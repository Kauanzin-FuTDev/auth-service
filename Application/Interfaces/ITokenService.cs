using System.Security.Claims;
using Domain.Entities;

namespace Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
    ClaimsIdentity GenerateClaimsIdentity(User user);
}