using Newtonsoft.Json.Converters;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Authorization.Core.Formatter
{
    public static class FormatterConfig
	{
		public static void Register(HttpConfiguration config)
		{
			var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
			config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

			config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new IsoDateTimeConverter
			{
				DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'"
			});

			config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
		}
	}
}
