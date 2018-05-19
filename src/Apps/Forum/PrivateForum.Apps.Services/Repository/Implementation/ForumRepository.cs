using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using PrivateForum.App.Web.Services.Models.Forum;
using PrivateForum.App.Web.Services.Repository.Abstraction;
using PrivateForum.Core;
using PrivateForum.Core.Framework.Security;

namespace PrivateForum.App.Web.Services.Repository.Implementation
{
    public class ForumRepository : IForumRepository
    {
        private readonly DbConnection connection;
        private readonly IAuditContext audit;

        public ForumRepository(IConnectionFactory connection, IAuditContext auditContext)
        {
            this.connection = connection.Connection;
            this.audit = auditContext;
        }


        public async Task<bool> Create(CreateForumVM forum)
        {
            var parameters = new DynamicParameters(new { name = forum.Name, parentId = forum.ParentId, userId = forum.UserId });

            return await connection.ExecuteScalarAsync<bool>("sp_Forum_ForumAppRepository_CreateForum", parameters.AuditInsert(audit), commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Delete(int id)
        {
            var parameters = new DynamicParameters(new { forumId = id });

            return await connection.ExecuteScalarAsync<bool>("sp_Forum_ForumAppRepository_DeleteForum", parameters.AuditUpdate(audit), commandType: CommandType.StoredProcedure);
        }

        public async Task<ForumVM> GetById(int id,int userId)
        {
            var parameters = new DynamicParameters(new { forumId = id, userId = userId });
            var reader =  await connection.QueryMultipleAsync("sp_Forum_ForumAppRepository_GetForumById", param: parameters, commandType: CommandType.StoredProcedure);

            var result = await reader.ReadFirstOrDefaultAsync<ForumVM>();
            result.SubForums = await reader.ReadAsync<BaseForumVM>();
            result.Topics = await reader.ReadAsync<ForumTopicVM>();

            return result;
        }

        public async Task<ForumVM> GetRootForumAsync(int userId)
        {
            var parameters = new DynamicParameters(new { userId = userId });
            var reader = await connection.QueryMultipleAsync("sp_Forum_ForumAppRepository_GetRootForum", param: parameters, commandType: CommandType.StoredProcedure);

            var result = await reader.ReadFirstOrDefaultAsync<ForumVM>();
            result.SubForums = await reader.ReadAsync<BaseForumVM>();
            result.Topics = await reader.ReadAsync<ForumTopicVM>();

            return result;
        }

        public async Task<bool> Update(EditForumVM forum)
        {
            var parameters = new DynamicParameters(new { forumId = forum.Id, name = forum.Name });

            return await connection.ExecuteScalarAsync<bool>("sp_Forum_ForumAppRepository_EditForum", parameters.AuditUpdate(audit), commandType: CommandType.StoredProcedure);
        }
    }
}
