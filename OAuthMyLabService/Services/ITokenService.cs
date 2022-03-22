using OAuthMyLabService.Models;

namespace OAuthMyLabService.Services
{
    public interface ITokenService
    {
        Task<string> BuildTokenAsync(Credentials credentials, OAuthSettings settings);
    }
}
