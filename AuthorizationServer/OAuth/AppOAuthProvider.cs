using AuthorizationServer.Models;
using AuthorizationServer.Services;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationServer.OAuth
{
    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly ILoginService _loginService;
        private readonly IAudienceStore _audienceStore;

        public AppOAuthProvider(
            ILoginService loginService,
            IAudienceStore audienceStore)
        {
            _loginService = loginService;
            _audienceStore = audienceStore;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (
                context.TryGetBasicCredentials(out string clientId, out _) ||
                context.TryGetFormCredentials(out clientId, out _))
            {
                var audience = _audienceStore.Find(clientId);

                if (audience == null)
                {
                    context.SetError("invalid_clientId", string.Format("Invalid client_id '{0}'", context.ClientId));
                    context.Rejected();
                    return Task.FromResult<object>(null);
                }
            }
            else
            {
                context.SetError("invalid_clientId", "client_Id is not set");
                context.Rejected();
                return Task.FromResult<object>(null);
            }

            context.Validated();
            return Task.FromResult<object>(null);
        }

        async public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var loginState = await _loginService.LoginAsync(new LoginCommand
            {
                UserName = context.UserName,
                Password = context.Password
            });
            if (!loginState.Logged)
            {
                context.SetError("invalid_credentials", "Invalid credentials");
                context.Rejected();
                return;
            }

            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));

            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                         "audience", context.ClientId ?? string.Empty
                    }
                });

            var ticket = new AuthenticationTicket(identity, props);

            context.Validated(ticket);
        }
    }
}