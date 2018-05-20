using System;
using System.Collections.Generic;
using System.Text;

namespace PrivateForum.Apps.Services.Models.Identity.VM
{
    public class UserLoginVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public string RedirectUrl { get; set; }
    }
}
