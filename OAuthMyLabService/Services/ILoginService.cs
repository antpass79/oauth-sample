using OAuthMyLabService.Models;

namespace OAuthMyLabService.Services
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(Credentials credentials);
    }
}