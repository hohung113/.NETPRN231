using System.Security.Claims;

namespace eStoreAPI.JWTServices
{
    public interface IJWTService
    {
        string GenerateToken(string username);
        ClaimsPrincipal ValidateToken(string token);
    }
}
