namespace Ixcent.CryptoTerminal.Api.Middlewares
{
    using Application.Exceptions;
    using Newtonsoft.Json;
    using System.Net;

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
            catch (RestException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string result = "";
            switch (exception)
            {
                case RestException validationException:
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)validationException.Code;
                    result = JsonConvert.SerializeObject(new { errors = validationException.Errors });
                    break;
                default:
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            return context.Response.WriteAsync(result);
        }
    }
}
