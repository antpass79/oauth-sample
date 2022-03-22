using System.Web.Http;
using System.Web.Http.Controllers;

namespace ResourceServer.OAuth
{
    public class ToggleAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly bool _disableAuthentication;
        public ToggleAuthorizeAttribute(bool disableAuthentication)
        {
            _disableAuthentication = disableAuthentication;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return _disableAuthentication || base.IsAuthorized(actionContext);
        }
    }
}