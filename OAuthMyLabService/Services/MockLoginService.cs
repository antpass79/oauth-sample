using Microsoft.Extensions.Options;
using OAuthMyLabService.Models;

namespace OAuthMyLabService.Services
{
    public class MockLoginService : ILoginService
    {
        private readonly IOptions<OAuthSettings> _settingsService;

        public MockLoginService(IOptions<OAuthSettings> settingsService)
        {
            _settingsService = settingsService;
        }

        async public Task<bool> LoginAsync(Credentials credentials)
        {
            return await Task.FromResult(
                _settingsService.Value.UserName == credentials.UserName &&
                _settingsService.Value.Password == credentials.Password);
        }
    }
}