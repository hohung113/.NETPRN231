using System.Security.Claims;

namespace eStoreAPI.JWTServices
{
    public interface IJWTService
    {
        string GenerateToken(string username, int userID);
        ClaimsPrincipal ValidateToken(string token);
    }
}
