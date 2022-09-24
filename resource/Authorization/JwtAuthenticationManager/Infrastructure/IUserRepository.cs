using JwtAuthenticationManager.Models;

namespace JwtAuthenticationManager.Infrastructure
{
    public interface IUserRepository
    {
        Task<string> GetUserRole(string UserName, string Password);
        Task<bool> CreateUser(CreateUserRequest User);
    }
}
