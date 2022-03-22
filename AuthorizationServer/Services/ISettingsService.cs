using Authorization.Core.Models;
using AuthorizationServer.Models;

namespace AuthorizationServer.Services
{
    public interface ISettingsService
	{
		WebServerStartingParameters ReadWebServerStartingParameters();
		OAuthSettings ReadOAuthSettings();
	}
}