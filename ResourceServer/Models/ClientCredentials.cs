namespace ResourceServer.Models
{
    public class ClientCredentials
    {
		public bool DisableAuthentication
		{
			get;
			set;
		}
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string ClientSecret { get; set; }
	}
}