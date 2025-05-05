using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace FinalLabAppWebServ.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private const string TokenKey = "Authorization";
        private const string ValidToken = "BearerMiToken1";

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Options)
            {
                await _next(context);
                return;
            }

            var token = context.Request.Headers[TokenKey].FirstOrDefault();

            if (string.IsNullOrEmpty(token) || token != ValidToken)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token inválido o faltante");
                return;
            }

            await _next(context);
        }
    }
}
