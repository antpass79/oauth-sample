using Authorization.Core.Models;
using AuthorizationServer.Models;
using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace AuthorizationServer.Services
{
    public class JsonFileSettingsService : ISettingsService
	{
		private readonly AppSettingsModel _appSettingsModel;

		public JsonFileSettingsService()
		{
			var currentPath = Assembly.GetExecutingAssembly().Location;
			var currentDirectory = Path.GetDirectoryName(currentPath);
			var appSettings = $"{currentDirectory}/appSettings.json";

			using (var streamReader = new StreamReader(appSettings))
			{
				string json = streamReader.ReadToEnd();
				_appSettingsModel = JsonConvert.DeserializeObject<AppSettingsModel>(json);
			}
		}

		public WebServerStartingParameters ReadWebServerStartingParameters()
		{
			return _appSettingsModel.WebServerStartingParameters;
		}

		public OAuthSettings ReadOAuthSettings()
		{
			return _appSettingsModel.OAuthSettings;
		}
	}
}