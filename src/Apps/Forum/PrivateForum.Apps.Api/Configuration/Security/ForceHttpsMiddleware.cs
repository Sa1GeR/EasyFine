using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Configuration.Security
{
    public class ForceHttpsMiddleware
    {
        private readonly RequestDelegate _next;

        public ForceHttpsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.IsHttps)
            {
                await _next(context);
            }
            else
            {
                var withHttps = "https://" + context.Request.Host + context.Request.Path;
                context.Response.Redirect(withHttps);
            }
        }
    }

    public static class ForceHttpsExtensions
    {
        public static IApplicationBuilder UseForceHttpsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ForceHttpsMiddleware>();
        }
    }
}
