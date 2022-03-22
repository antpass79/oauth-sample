using Microsoft.Owin.Hosting;
using ResourceServer.Services;
using System;
using System.Linq;

namespace ResourceServer
{
    public class Program
	{
		static void Main(string[] args)
		{
			var settingsService = new JsonFileSettingsService();
			var url = ReadUrl(args) ?? settingsService.ReadWebServerStartingParameters().Url;
			using (WebApp.Start<Startup>(url))
			{
				Console.WriteLine($"Server listen on {url}");
				Console.ReadLine();
			}
		}

		static string ReadUrl(string[] args)
		{
			if (args.Count() < 2)
			{
				return null;
			}

			if (args[0] == "-u")
			{
				return args[1];
			}

			return null;
		}
	}
}