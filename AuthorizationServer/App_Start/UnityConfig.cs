using Authorization.Core.Services;
using AuthorizationServer.Models;
using AuthorizationServer.Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace AuthorizationServer
{
    public static class UnityConfig
	{
		public static readonly IUnityContainer Container = new UnityContainer();

		public static void RegisterComponents(HttpConfiguration configuration)
		{
			Container
				.RegisterSingleton<ISettingsService, JsonFileSettingsService>()
				.RegisterSingleton<ILoginService, MockLoginService>()
				.RegisterSingleton<IAudienceStore, AudienceStore>()
				.RegisterSingleton<ILogService, ConsoleLogService>();

			configuration.DependencyResolver = new UnityDependencyResolver(Container);
		}
	}
}