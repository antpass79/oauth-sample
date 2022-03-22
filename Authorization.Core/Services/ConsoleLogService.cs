using System;
using System.Threading.Tasks;

namespace Authorization.Core.Services
{
    public class ConsoleLogService : ILogService
    {
        public Task ErrorAsync(string message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }

        public Task ExceptionAsync(Exception exception)
        {
            Console.WriteLine(exception.Message);
            return Task.CompletedTask;
        }

        public Task InfoAsync(string message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }

        public Task WarnAsync(string message)
        {
            Console.WriteLine(message);
            return Task.CompletedTask;
        }
    }
}
