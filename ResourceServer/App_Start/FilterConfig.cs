using Authorization.Core.Services;
using ResourceServer.Models;
using ResourceServer.OAuth;
using ResourceServer.Services;
using System.Web.Http;
using System.Web.Mvc;

namespace ResourceServer
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(HttpConfiguration configuration)
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());

            var settingsService = WebServiceLocator.Resolve<ISettingsService>();
            var disableAuthentication = settingsService.ReadClientCredentials().DisableAuthentication;

            configuration.Filters.Add(new ToggleAuthorizeAttribute(disableAuthentication));
        }
    }
}
