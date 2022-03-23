using Microsoft.AspNetCore.WebUtilities;

namespace OAuthMyLabService.Services
{
    public class ClientSecretBase64Encoder : IClientSecretEncoder
    {
        public byte[] Decode(string secret)
        {
            return WebEncoders.Base64UrlDecode(secret);
        }
    }
}
