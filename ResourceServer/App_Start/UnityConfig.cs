using Authorization.Core.Services;
using ResourceServer.Models;
using ResourceServer.Services;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ResourceServer
{
    public static class UnityConfig
	{
		public static readonly IUnityContainer Container = new UnityContainer();

		public static void RegisterComponents(HttpConfiguration configuration)
		{
			Container
				.RegisterSingleton<ISettingsService, JsonFileSettingsService>()
				.RegisterSingleton<ILogService, ConsoleLogService>();

			configuration.DependencyResolver = new UnityDependencyResolver(Container);
		}
	}
}