using AuthorizationServer.Models;
using System.Threading.Tasks;

namespace AuthorizationServer.Services
{
    public interface ILoginService
    {
        Task<LoginState> LoginAsync(LoginCommand command);
    }
}