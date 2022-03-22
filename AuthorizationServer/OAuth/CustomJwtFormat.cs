using AuthorizationServer.Models;
using AuthorizationServer.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace AuthorizationServer.OAuth
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private const string AudiencePropertyKey = "audience";

        private readonly string _issuer = string.Empty;
        private readonly IAudienceStore _audienceStore;

        public CustomJwtFormat(
            string issuer,
            IAudienceStore audienceStore)
        {
            _issuer = issuer;
            _audienceStore = audienceStore;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            string audienceClientId =
                data.Properties.Dictionary.ContainsKey(AudiencePropertyKey) ?
                data.Properties.Dictionary[AudiencePropertyKey] :
                null;

            if (string.IsNullOrWhiteSpace(audienceClientId))
                throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");

            Audience audience = _audienceStore.Find(audienceClientId);

            string symmetricKeyAsBase64 = audience.ClientSecret;

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            var securityKey = new SymmetricSecurityKey(keyByteArray);
            var signingKey = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var issued = data.Properties.IssuedUtc;
            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(
                _issuer,
                audienceClientId,
                data.Identity.Claims,
                issued.Value.UtcDateTime,
                expires.Value.UtcDateTime,
                signingKey);

            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);

            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}