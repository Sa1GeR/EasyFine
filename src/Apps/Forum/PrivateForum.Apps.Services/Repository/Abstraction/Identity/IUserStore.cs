using Microsoft.AspNetCore.Identity;
using PrivateForum.Apps.Services.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrivateForum.Apps.Services.Repository.Abstraction.Identity
{
    public interface IUserStore: IUserStore<User>, IUserRoleStore<User>, IUserPasswordStore<User>, IUserEmailStore<User>
    {
    }
}
