using System.Web.Http;
using System.Web.Mvc;

namespace AuthorizationServer
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(HttpConfiguration configuration)
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());

            configuration.Filters.Add(new System.Web.Http.AuthorizeAttribute());
        }
    }
}
