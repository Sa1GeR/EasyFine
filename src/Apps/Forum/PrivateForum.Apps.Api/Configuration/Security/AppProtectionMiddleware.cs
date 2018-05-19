using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Configuration.Security
{
    public class AppProtectionMiddleware
    {
        private readonly RequestDelegate _next;

        public AppProtectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IAuthorizationService authorizationService)
        {
            var authorized = await authorizationService.AuthorizeAsync(context.User, null, "AppProtectionPolicy");
            if (!authorized.Succeeded)
            {
                await context.ChallengeAsync();

                if (context.Request.Path.StartsWithSegments(new PathString("/api")))
                {
                    context.Response.StatusCode = 401;
                }

                return;
            }

            await _next(context);
            return;
        }
    } 

    public static class AppProtectionExtensions
    {
        public static IApplicationBuilder UseAppProtectionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AppProtectionMiddleware>();
        }
    }
}
