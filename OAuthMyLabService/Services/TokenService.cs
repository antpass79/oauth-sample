using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using OAuthMyLabService.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OAuthMyLabService.Services
{
    public class TokenService : ITokenService
    {
        public Task<string> BuildTokenAsync(Credentials credentials, OAuthSettings settings)
        {
            var claims = new[] {
                new Claim(ClaimTypes.Name, credentials.UserName ?? String.Empty),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };

            var audience = settings
                .Audiences?
                .FirstOrDefault(audience => audience.ClientId == credentials.ClientId);
            if (audience == null)
                return Task.FromResult(string.Empty);

            var symmetricKeyAsBase64 = audience.ClientSecret ?? string.Empty;
            var keyByteArray = WebEncoders.Base64UrlDecode(symmetricKeyAsBase64);

            var securityKey = new SymmetricSecurityKey(keyByteArray);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                settings.Issuer,
                audience.ClientId,
                claims,
                expires: DateTime.Now.AddMinutes(settings.ExpiryDurationMinutes),
                signingCredentials: signingCredentials);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(tokenDescriptor));
        }
    }
}
