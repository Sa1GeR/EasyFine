using PrivateForum.Core.Framework.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrivateForum.App.Web
{
    public class AuditContext : IAuditContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuditContext(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string DisplayName
        {
            get
            {
                var givenNameClaim = httpContextAccessor.HttpContext.User.FindFirst("given_name");
                var familiyNameClaim = httpContextAccessor.HttpContext.User.FindFirst("family_name");

                if(givenNameClaim != null && familiyNameClaim != null)
                {
                    return $"{givenNameClaim.Value} {familiyNameClaim.Value}";
                }


                return ModifiedBy;
            }
        }

        public string FirstName
        {
            get
            {
                var givenNameClaim = httpContextAccessor.HttpContext.User.FindFirst("given_name");

                if(givenNameClaim != null)
                {
                    return givenNameClaim.Value;
                }

                return ModifiedBy;
            }
        }

        public string Id
        {
            get
            {
                var nameClaim = httpContextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                if (nameClaim != null)
                    return nameClaim.Value;

                return "0";
            }
        }

        public IEnumerable<string> Roles
        {
            get
            {
                var roles = httpContextAccessor.HttpContext.User.FindAll("role");
                if (roles != null && roles.Count() > 0)
                    return roles.SelectMany(p => p.Value.Split(','));

                return new List<string>();
            }
        }

        public virtual DateTime ModificationDate
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
        public string ModifiedBy
        {
            get
            {
                var nameClaim = httpContextAccessor.HttpContext.User.FindFirst("name");
                if (nameClaim != null)
                    return nameClaim.Value;

                return httpContextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString() ?? string.Empty;
            }
        }

    }

}