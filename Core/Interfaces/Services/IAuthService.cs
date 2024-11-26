using Core.Models;

namespace Core.Interfaces.Services;

public interface IAuthService
{
    string CreateToken(User user);
    bool ValidateJwt(string token);
}
