using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Firotechbd.Services
{
    public class LinkBlockerMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly HashSet<string> BlockedLinks = new HashSet<string>
        {
            "/Identity/Account/Register",
            // Add more links here as needed
        };

        public LinkBlockerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (BlockedLinks.Contains(context.Request.Path))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden; // Forbidden
                await context.Response.WriteAsync("Access to this page is blocked.");
                return; // Skip the next middleware
            }

            await _next(context); // Call the next middleware
        }
    }
}
