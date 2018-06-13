using PrivateForum.Core;
using PrivateForum.Core.Framework.Security;
using PrivateForum.App.Web.Services.Models;
using PrivateForum.App.Web.Services.Repository.Abstraction;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using PrivateForum;
using System.IO;
using System.Collections.Generic;
using PrivateForum.Apps.Services.Models.Identity;

namespace PrivateForum.App.Web.Services.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnection connection;
        private readonly IAuditContext audit;

        public UserRepository(IConnectionFactory connection,  IAuditContext auditContext)
        {
            this.connection = connection.Connection;
            this.audit = auditContext;
        }

        public async Task<bool> BlockUser(int id)
        {
            var parameters = new DynamicParameters(new { userId = id });
            return await connection.ExecuteScalarAsync<bool>("sp_Forum_UsersAppRepository_BlockUser", parameters.AuditUpdate(audit), commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> DeleteUser(int id)
        {
            var parameters = new DynamicParameters(new { userId = id });
            return await connection.ExecuteScalarAsync<bool>("sp_Forum_UsersAppRepository_DeleteUser", parameters.AuditUpdate(audit), commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await connection.QueryAsync<User>("sp_Forum_UsersAppRepository_GetAll", null , commandType: CommandType.StoredProcedure);
        }

        public async Task<CurentUserVM> GetCurrentUserAsync(int userId)
        {
            var parameters = new
            {
                UserId = userId
            };
            
            using (var reader = await connection.QueryMultipleAsync("sp_Security_UserStore_FindByIdAsync", param: parameters, commandType: CommandType.StoredProcedure))
            {
                    CurentUserVM currentUser = await reader.ReadSingleAsync<CurentUserVM>();
                    currentUser.Roles = await reader.ReadAsync<RoleVM>();

                    return currentUser;
            }
        }

        public async Task<UserProfileVM> GetUserProfile(int id)
        {
            var parameters = new DynamicParameters(new { userId = id });
            return await connection.QueryFirstOrDefaultAsync<UserProfileVM>("sp_Forum_UsersAppRepository_GetUserProfile", param: parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> SaveUserAvatar(int id, string url)
        {
            var parameters = new DynamicParameters(new { userId = id, avatarUrl = url });
            return await connection.ExecuteScalarAsync<bool>("sp_Forum_UsersAppRepository_UploadAvatar", parameters.AuditUpdate(audit), commandType: CommandType.StoredProcedure);
        }
    }
}
