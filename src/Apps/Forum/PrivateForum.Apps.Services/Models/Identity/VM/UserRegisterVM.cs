using System;
using System.Collections.Generic;
using System.Text;

namespace PrivateForum.Apps.Services.Models.Identity.VM
{
    public class UserRegisterVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

        public DateTime Birthday { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }
    }
}
