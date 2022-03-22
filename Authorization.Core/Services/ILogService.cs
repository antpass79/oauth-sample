using System;
using System.Threading.Tasks;

namespace Authorization.Core.Services
{
    public interface ILogService
    {
        Task InfoAsync(string message);
        Task WarnAsync(string message);
        Task ErrorAsync(string message);
        Task ExceptionAsync(Exception exception);
    }
}
