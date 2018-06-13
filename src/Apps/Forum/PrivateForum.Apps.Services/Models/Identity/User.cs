using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace PrivateForum.Apps.Services.Models.Identity
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime Birthday { get; set; }

        public IReadOnlyCollection<string> UserRoles { get; private set; }

        public User()
        {
            this.DateCreated = DateTime.UtcNow;
        }

        public void SetPasswordHash(string passwordHash)
        {
            this.PasswordHash = passwordHash;
        }
        public void SetIdentificator(int id)
        {
            this.Id = id;
        }

        public void SetEmail(string email)
        {
            this.Email = email;
        }

        public void SetUserName(string userName)
        {
            this.UserName = userName;
        }

        public void SetRoles(IEnumerable<string> roles)
        {
            this.UserRoles = new ReadOnlyCollection<string>(roles.ToList());
        }

    }
}
