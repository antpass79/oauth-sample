using Swashbuckle.Application;
using System.Web.Http;

namespace Authorization.Core.Swagger
{
    public class SwaggerConfig
	{
		public static void Register(HttpConfiguration configuration, bool secure)
		{
			var thisAssembly = typeof(SwaggerConfig).Assembly;

			configuration
			.EnableSwagger(c =>
			{
				c.OperationFilter<SwaggerHeaderOperationFilter>();
				if (secure)
                {
					c.DocumentFilter<SwaggerTokenDocumentFilter>();
				}

				c.SingleApiVersion("v1", "Authorization Api V1");
			})
			.EnableSwaggerUi(c =>
			{
			});
		}
	}
}
