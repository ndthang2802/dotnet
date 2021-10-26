using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Application.helpers
{
    public class SignalRQueryStringAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public SignalRQueryStringAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers["Connection"] == "Upgrade" && 
                context.Request.Query.TryGetValue("access_token", out var token))
            {
                context.Request.Headers.Add("Authorization", "Bearer " + token.First());
            }
            await _next.Invoke(context);
        }
    }

}