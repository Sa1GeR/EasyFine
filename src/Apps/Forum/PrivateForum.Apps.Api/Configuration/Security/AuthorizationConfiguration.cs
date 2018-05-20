using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using PrivateForum.Core.Utilities;
using PrivateForum.Apps.Services.Models.Identity;
using Microsoft.AspNetCore.Identity;
using PrivateForum.Apps.Services.Repository.Implementation.Identity;

namespace PrivateForum.App.Web.Configuration.Security
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection ConfigureForumAuthorization(this IServiceCollection services)
        {
            services
                .AddIdentity<User, Role>();

            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<Role>, RoleStore>();

            services
                .AddAuthorization(options =>
                {
                    //options.AddPolicy("AppProtectionPolicy", policy => policy.RequireClaim("name"));
                })
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie();

            return services;
        }
    }
}
