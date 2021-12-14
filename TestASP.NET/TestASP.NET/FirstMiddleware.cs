using TestASP.NET.Services;

namespace TestASP.NET
{
    public class FirstMiddleware
    {
        private RequestDelegate _next;

        private IMessageSender _sender;

        public FirstMiddleware(RequestDelegate next, IMessageSender messageSender)
        {
            _next = next;
            _sender = messageSender;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _sender?.Send();
            if (context.Request.Query["Hello"] == "world")
            {
                await context.Response.WriteAsync("Hi");
            }
            else
            {
                await _next(context);
            }
        }
    }

    public static class FirstMiddlewareExtensions
    {
        public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FirstMiddleware>();
        }
    }
}
