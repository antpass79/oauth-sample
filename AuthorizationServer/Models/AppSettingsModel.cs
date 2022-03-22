using Authorization.Core.Models;

namespace AuthorizationServer.Models
{
	public class AppSettingsModel
	{
		public WebServerStartingParameters WebServerStartingParameters
		{
			get;
			set;
		}
		public OAuthSettings OAuthSettings
		{
			get;
			set;
		}
	}
}