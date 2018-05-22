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
using System.Security.Claims;

namespace PrivateForum.App.Web.Configuration
{
    public class ForumUserClaimsPrincipalFactory: UserClaimsPrincipalFactory<User, Role>
    {
        public ForumUserClaimsPrincipalFactory(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);

            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            });

            return principal;
        }
    }
}
