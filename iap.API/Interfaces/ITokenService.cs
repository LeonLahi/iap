using iap.API.Models;

namespace iap.API.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}