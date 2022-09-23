using AuthAPI.Model;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace AuthAPI.Infrastructure
{
    public interface IUserRepository
    {
        Task<string> GetUserRole(string UserName, string Password);
        Task<bool> CreateUser(CreateUserModel User);
    }
}
