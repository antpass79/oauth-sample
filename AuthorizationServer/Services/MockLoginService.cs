using Authorization.Core.Services;
using AuthorizationServer.Models;
using System.Threading.Tasks;

namespace AuthorizationServer.Services
{
    public class MockLoginService : ILoginService
    {
        private readonly ISettingsService _settingsService;

        public MockLoginService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        async public Task<LoginState> LoginAsync(LoginCommand command)
        {
            var oathSettings = _settingsService.ReadOAuthSettings();
            return await Task.FromResult(new LoginState
            {
                Logged = oathSettings.UserName == command.UserName && oathSettings.Password == command.Password
            });
        }
    }
}