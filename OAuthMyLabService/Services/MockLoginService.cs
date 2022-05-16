using Microsoft.Extensions.Options;
using OAuthMyLabService.Models;

namespace OAuthMyLabService.Services
{
    public class MockLoginService : ILoginService
    {
        private readonly ILogger<MockLoginService> _logger;
        private readonly IOptions<OAuthSettings> _settingsService;

        public MockLoginService(
            ILogger<MockLoginService> logger,
            IOptions<OAuthSettings> settingsService)
        {
            _logger = logger;
            _settingsService = settingsService;
        }

        async public Task<bool> LoginAsync(Credentials credentials)
        {
            Console.WriteLine($"UserName from credentials: {credentials.UserName}, Password from credentials: {credentials.Password}");
            Console.WriteLine($"UserName from settings: {_settingsService.Value.UserName}, Password from settings: {_settingsService.Value.Password}");

            return await Task.FromResult(
                _settingsService.Value.UserName == credentials.UserName &&
                _settingsService.Value.Password == credentials.Password);
        }
    }
}