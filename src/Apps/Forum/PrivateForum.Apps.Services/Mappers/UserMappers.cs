using PrivateForum.Apps.Services.Models.Identity;
using PrivateForum.Apps.Services.Models.Identity.VM;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrivateForum.Apps.Services.Mappers
{
    public static class UserMappers
    {
        public static User ToDomain(this UserRegisterVM user)
        {
            return new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email
            };
        }
    }
}
