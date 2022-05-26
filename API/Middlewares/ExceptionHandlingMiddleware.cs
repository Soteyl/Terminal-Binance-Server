using Newtonsoft.Json;
using System.Net;
using Serilog;

namespace Ixcent.CryptoTerminal.Api.Middlewares
{
    using Application.Exceptions;

    /// <summary>
    /// Handles all request exceptions. If this is a specific <see cref="RestException"/>,
    /// then shows needed information to user in API response.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result = "";
            context.Response.ContentType = "application/json";
            switch (exception)
            {
                case RestException validationException:
                    context.Response.StatusCode = (int)validationException.StatusCode;
                    result = JsonConvert.SerializeObject(
                        new
                        {
                            code = validationException.ErrorCode,
                            errors = validationException.Errors
                        });
                    break;
                default:
                    Log.Error(exception.ToString());
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            return context.Response.WriteAsync(result);
        }
    }
}
