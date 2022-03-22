using Authorization.Core.Models;
using ResourceServer.Models;

namespace ResourceServer.Services
{
    public interface ISettingsService
	{
		WebServerStartingParameters ReadWebServerStartingParameters();
		ClientCredentials ReadClientCredentials();
	}
}