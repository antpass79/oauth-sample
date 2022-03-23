using System.Collections.Generic;

namespace ResourceServer.Models
{
    public class ClientCredentials
    {
		public bool DisableAuthentication
		{
			get;
			set;
		}
		public string Audience { get; set; }
		public IEnumerable<IssuerProvider> IssuerProviders { get; set; }
	}
}