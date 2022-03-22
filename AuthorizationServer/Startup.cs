using Authorization.Core.Formatter;
using Authorization.Core.Middlewares;
using Authorization.Core.Services;
using Authorization.Core.Swagger;
using AuthorizationServer.Models;
using AuthorizationServer.OAuth;
using AuthorizationServer.Services;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using System.Web.Routing;

[assembly: OwinStartup(typeof(AuthorizationServer.Startup))]
namespace AuthorizationServer
{
    public class Startup
	{
		public static OAuthBearerAuthenticationOptions OAuthBearerAuthenticationOptions { get; private set; }

		public void Configuration(IAppBuilder app)
		{
			var configuration = new HttpConfiguration();
			UnityConfig.RegisterComponents(configuration);

			ConfigureOAuth(app);

			WebApiConfig.Register(configuration);
			SwaggerConfig.Register(configuration, true);
			FormatterConfig.Register(configuration);
			FilterConfig.RegisterGlobalFilters(configuration);

			app
			.Use<ExceptionMiddleware>(WebServiceLocator.Resolve<ILogService>())
			.UseWebApi(configuration);
		}

		static void ConfigureOAuth(IAppBuilder app)
		{
			var settingsService = WebServiceLocator.Resolve<ISettingsService>();
			var audienceStore = WebServiceLocator.Resolve<IAudienceStore>();
			var loginService = WebServiceLocator.Resolve<ILoginService>();

			var oAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions
			{
				AuthenticationMode = AuthenticationMode.Active,
				TokenEndpointPath = new PathString("/auth/token"),
				Provider = new AppOAuthProvider(loginService, audienceStore),
				AccessTokenExpireTimeSpan = TimeSpan.FromHours(8),
				AllowInsecureHttp = !settingsService.ReadWebServerStartingParameters().Secure,
				AccessTokenFormat =	new CustomJwtFormat(settingsService.ReadOAuthSettings().Issuer, audienceStore)
			};

			app.UseOAuthAuthorizationServer(oAuthAuthorizationServerOptions);
		}
	}
}