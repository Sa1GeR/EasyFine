using System;

namespace PrivateForum.App.Web.Services.Models
{
    public class UserProfileVM : BaseModelVM
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsBlocked { get; set; }
        public string AvatarUrl { get; set; }
    }
}
