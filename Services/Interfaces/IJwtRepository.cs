using Softka.Models;
using Softka.Models.DTOs;

namespace Softka.Services
{
    public interface IJwtRepository
    {
        string GenerateToken (UserDto user);
    }
}
