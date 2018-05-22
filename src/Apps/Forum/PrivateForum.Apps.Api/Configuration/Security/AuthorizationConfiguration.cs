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
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;

namespace PrivateForum.App.Web.Configuration.Security
{
    public static class AuthorizationConfiguration
    {
        public static IServiceCollection ConfigureForumAuthorization(this IServiceCollection services)
        {
            services
                .AddIdentity<User, Role>()
                .AddDefaultTokenProviders()
                .AddClaimsPrincipalFactory<ForumUserClaimsPrincipalFactory>();

            services.AddTransient<IUserStore<User>, UserStore>();
            services.AddTransient<IRoleStore<Role>, RoleStore>();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, ForumUserClaimsPrincipalFactory>();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "http://localhost:1488",
                        ValidAudience = "http://localhost:1488",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HnczRNSZ96EMCPw6H6nJQw2tbTDHIFJVknKxcN59RfPjuOE7xzs8ZwZfkQEyY0e")),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            return services;
        }
    }
}
