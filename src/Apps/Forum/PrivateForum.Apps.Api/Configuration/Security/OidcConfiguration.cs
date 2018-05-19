using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Configuration.Security
{
    public class OidcConfiguration
    {
        public string Authority { get; set; }
        public string PostLogoutRedirectUri { get; set; }
        public string ResponseType { get; set; }
        public string ClientSecret { get; set; }
        public string ClientId { get; set; }
    }
}
