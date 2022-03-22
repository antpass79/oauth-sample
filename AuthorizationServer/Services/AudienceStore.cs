using Authorization.Core.Models;
using AuthorizationServer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationServer.Services
{
    public class AudienceStore : IAudienceStore
    {
        private readonly IList<Audience> _audiences;

        public AudienceStore(ISettingsService settingsService)
        {
            _audiences = settingsService
                .ReadOAuthSettings()
                .Audiences
                .ToList();
        }

        public Audience Find(string clientId)
        {
            return _audiences.FirstOrDefault(audience => audience.ClientId == clientId);
        }

        async public Task<Audience> FindAsync(string clientId)
        {
            return await Task.FromResult(Find(clientId));
        }
    }
}