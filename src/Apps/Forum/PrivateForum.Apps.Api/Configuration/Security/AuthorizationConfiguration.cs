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

namespace PrivateForum.App.Web.Configuration.Security
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection ConfigureForumAuthorization(this IServiceCollection services)
        {
            OidcConfiguration configuration = services.Resolve<IOptions<OidcConfiguration>>()?.Value;
            if (configuration == null)
                throw new ArgumentException("Oidc configuration");

            services
                .AddAuthorization(options =>
                {
                    options.AddPolicy("AppProtectionPolicy", policy => policy.RequireClaim("name"));
                })
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddOpenIdConnect(options =>
                {
                    options.Authority = configuration.Authority;

                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.SignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.RequireHttpsMetadata = false;

                    options.ClientId = configuration.ClientId;
                    options.ClientSecret = configuration.ClientSecret;
                    options.ResponseType = configuration.ResponseType;
                    options.SignedOutRedirectUri = configuration.PostLogoutRedirectUri;

                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;
                    options.Scope.AddRange(new string[] { "profile", "ed-forum-api" });
                })
                .AddCookie();

            return services;
        }
    }
}
