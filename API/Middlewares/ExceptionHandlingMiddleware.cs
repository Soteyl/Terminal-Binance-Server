using System.Net;

using Ixcent.CryptoTerminal.Application.Exceptions;
using Ixcent.CryptoTerminal.Domain.Common;
using Ixcent.CryptoTerminal.Domain.Common.Models;

using Newtonsoft.Json;

using Serilog;

namespace Ixcent.CryptoTerminal.Api.Middlewares
{
    /// <summary>
    /// Handles all request exceptions. If this is a specific <see cref="ServerException"/>,
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
            Log.Error(exception.ToString()); // TODO ADD EMAIL SERVICE
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            result = JsonConvert.SerializeObject(
                Response.WithError(ServerResponseCode.InternalError));

            return context.Response.WriteAsync(result);
        }
    }
}