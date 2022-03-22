namespace OAuthMyLabService.Models
{
    public class WebServerStartingParameters
	{
		public string? ServerName
		{
			get;
			set;
		}
		public int Port
		{
			get;
			set;
		}
		public string? Schema
		{
			get;
			set;
		}
		public string Url => $"{Schema}://{ServerName}:{Port}/";
		public bool Secure => Schema?.ToLower() == "https";
	}
}
