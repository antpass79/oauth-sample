using System.Collections.Generic;

namespace OAuthMyLabService.Models
{
    public class OAuthSettings
	{
		public string? UserName
		{
			get;
			set;
		}
		public string? Password
		{
			get;
			set;
		}
		public string? Issuer
		{
			get;
			set;
		}
		public int ExpiryDurationMinutes
		{
			get;
			set;
		}
		public IEnumerable<Audience>? Audiences
		{
			get;
			set;
		}
	}
}