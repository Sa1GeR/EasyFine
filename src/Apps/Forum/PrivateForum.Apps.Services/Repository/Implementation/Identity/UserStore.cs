using Dapper;
using Microsoft.AspNetCore.Identity;
using PrivateForum.Apps.Services.Models.Identity;
using PrivateForum.Apps.Services.Repository.Abstraction.Identity;
using PrivateForum.Core;
using PrivateForum.Core.Framework.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrivateForum.Apps.Services.Repository.Implementation.Identity
{
    public class UserStore : IUserStore
    {
        private readonly DbConnection connection;
        private readonly IAuditContext audit;

        public UserStore(IConnectionFactory connection, IAuditContext auditContext)
        {
            this.connection = connection.Connection;
            this.audit = auditContext;
        }

        public async Task AddToRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            await this.connection.ExecuteAsync("sp_Security_UserStore_AddToRoleAsync", new { roleName = roleName, userId = user.Id }, commandType: CommandType.StoredProcedure);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            var parameters = new
            {
                Id = 0,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                PasswordHash = user.PasswordHash,
                UserName = user.UserName,
                DateCreated = user.DateCreated,
            };
            var userId = await this.connection.ExecuteScalarAsync<int>("sp_Security_UserStore_CreateAsync", parameters, commandType: CommandType.StoredProcedure);
            user.SetIdentificator(userId);

            return userId == default(int) ? IdentityResult.Failed() : IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            int affected = await this.connection.ExecuteAsync("sp_Security_UserStore_DeleteAsync", new { userId = user.Id }, commandType: CommandType.StoredProcedure);

            return affected == default(int) ? IdentityResult.Failed() : IdentityResult.Success;
        }

        public void Dispose()
        {}

        public async Task<User> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var parameters = new { email = normalizedEmail };
            var result = await this.connection.QueryMultipleAsync("sp_Security_UserStore_FindByEmailAsync", parameters, commandType: CommandType.StoredProcedure);

            var user = await result.ReadSingleOrDefaultAsync<User>();
            var roles = await result.ReadAsync<string>();

            if (user != null)
            {
                user.SetRoles(roles);
            }

            return user;
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var parameters = new { userId = userId };
            var result = await this.connection.QueryMultipleAsync("sp_Security_UserStore_FindByIdAsync", parameters, commandType: CommandType.StoredProcedure);

            var user = await result.ReadSingleOrDefaultAsync<User>();
            var roles = await result.ReadAsync<string>();

            if (user != null)
            {
                user.SetRoles(roles);
            }

            return user;
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var parameters = new { username = normalizedUserName };
            var result = await this.connection.QueryMultipleAsync("sp_Security_UserStore_FindByNameAsync", parameters, commandType: CommandType.StoredProcedure);

            var user = await result.ReadSingleOrDefaultAsync<User>();
            var roles = await result.ReadAsync<string>();

            if (user != null)
            {
                user.SetRoles(roles);
            }

            return user;
        }

        public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.UserRoles.ToList());
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            var parameters = new { roleName };
            var result = await this.connection.QueryMultipleAsync("sp_Security_UserStore_GetUsersInRole", parameters, commandType: CommandType.StoredProcedure);

            var users = await result.ReadAsync<User>();
            var roles = await result.ReadAsync<Tuple<string, int>>();

            foreach (User user in users)
            {
                var relatedRoles = roles.Where(r => r.Item2 == user.Id).Select(r => r.Item1);
                user.SetRoles(relatedRoles);
            }

            return users.ToList();
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
        }

        public Task<bool> IsInRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserRoles.Contains(roleName));
        }

        public async Task RemoveFromRoleAsync(User user, string roleName, CancellationToken cancellationToken)
        {
            await this.connection.ExecuteAsync("sp_Security_UserStore_RemoveFromRoleAsync", new { roleName = roleName, userId = user.Id }, commandType: CommandType.StoredProcedure);
        }

        public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.SetEmail(normalizedEmail);
            return Task.FromResult(0);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            user.SetUserName(normalizedName);
            return Task.FromResult(default(int));
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            user.SetPasswordHash(passwordHash);
            return Task.FromResult(default(int));
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            user.SetUserName(userName);
            return Task.FromResult(default(int));
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            var parameters = new
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                UserName = user.UserName,
                DateCreated = user.DateCreated
            };
            int affected = await this.connection.ExecuteAsync("sp_Security_UserStore_UpdateAsync", parameters, commandType: CommandType.StoredProcedure);

            return affected == default(int) ? IdentityResult.Failed() : IdentityResult.Success;
        }
    }
}
