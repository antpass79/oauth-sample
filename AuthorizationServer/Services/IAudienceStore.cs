using Authorization.Core.Models;
using AuthorizationServer.Models;
using System.Threading.Tasks;

namespace AuthorizationServer.Services
{
    public interface IAudienceStore
    {
        Audience Find(string clientId);
        Task<Audience> FindAsync(string clientId);
    }
}