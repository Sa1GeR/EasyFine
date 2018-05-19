using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Configuration
{
    public static class UserUrls
    {
        public const string GetCurrentUser = "user";
        public const string GetUserProfile = "profile/{id}";
        public const string BlockUser = "block/{id}";
        public const string DeleteUser = "user/{id}";

    }
}
