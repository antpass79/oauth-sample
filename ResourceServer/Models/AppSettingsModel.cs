using Authorization.Core.Models;

namespace ResourceServer.Models
{
    public class AppSettingsModel
	{
		public WebServerStartingParameters WebServerStartingParameters
		{
			get;
			set;
		}
		public ClientCredentials ClientCredentials
		{
			get;
			set;
		}
	}
}