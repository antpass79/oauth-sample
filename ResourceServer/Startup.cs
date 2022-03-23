using Authorization.Core.Formatter;
using Authorization.Core.Middlewares;
using Authorization.Core.Services;
using Authorization.Core.Swagger;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using ResourceServer.Models;
using ResourceServer.Services;
using System.Linq;
using System.Web.Http;

[assembly: OwinStartup(typeof(ResourceServer.Startup))]
namespace ResourceServer
{
    public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var configuration = new HttpConfiguration();
			UnityConfig.RegisterComponents(configuration);

			ConfigureOAuth(app);

			WebApiConfig.Register(configuration);
			SwaggerConfig.Register(configuration, false);
			FormatterConfig.Register(configuration);
			FilterConfig.RegisterGlobalFilters(configuration);

			app
			.Use<ExceptionMiddleware>(WebServiceLocator.Resolve<ILogService>())
			.UseWebApi(configuration);
		}

		static void ConfigureOAuth(IAppBuilder app)
		{
			var settingsService = WebServiceLocator.Resolve<ISettingsService>();
			var clientCredentials = settingsService.ReadClientCredentials();

			var audience = clientCredentials.Audience;

			app.UseJwtBearerAuthentication(
				new JwtBearerAuthenticationOptions
				{
					AuthenticationMode = AuthenticationMode.Active,
					AllowedAudiences = new[] { audience },
					IssuerSecurityKeyProviders = clientCredentials
						.IssuerProviders
						.Select(issuerProvider => new SymmetricKeyIssuerSecurityKeyProvider(
							issuerProvider.Issuer,
							TextEncodings.Base64Url.Decode(issuerProvider.Key)))
				});
		}
	}
}