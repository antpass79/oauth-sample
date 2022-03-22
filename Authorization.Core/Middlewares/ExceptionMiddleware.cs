using Authorization.Core.Services;
using Microsoft.Owin;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Authorization.Core.Middlewares
{
    public class ExceptionMiddleware : OwinMiddleware
	{
        private readonly ILogService _logService;

        public ExceptionMiddleware(
			OwinMiddleware next,
			ILogService logService)
		: base(next)
		{
            _logService = logService;
        }

		public override async Task Invoke(IOwinContext context)
		{
			try
			{
				await Next.Invoke(context);
			}
			catch (Exception exception)
			{
				try
				{
					await _logService.ExceptionAsync(exception);

					HandleException(exception, context);
					return;
				}
				catch (Exception innerException)
				{
					await _logService.ExceptionAsync(innerException);
				}
				throw;
			}
		}
		private void HandleException(Exception exception, IOwinContext context)
		{
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			context.Response.ReasonPhrase = "Internal Server Error";
			context.Response.ContentType = "application/json";
			context.Response.Write(exception.Message);
		}
	}
}
