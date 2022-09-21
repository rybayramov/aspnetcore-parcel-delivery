using Auth.Application.Models;
using System.Threading.Tasks;

namespace Auth.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
