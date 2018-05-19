using System;
using System.Collections.Generic;

namespace PrivateForum.App.Web.Services.Models
{
    public class CurentUserVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { private get; set; }
        public IEnumerable<RoleVM> Roles { get; set; }
    }
}
