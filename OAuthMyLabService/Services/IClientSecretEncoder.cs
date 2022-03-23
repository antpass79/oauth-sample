namespace OAuthMyLabService.Services
{
    public interface IClientSecretEncoder
    {
        byte[] Decode(string secret);
    }
}
