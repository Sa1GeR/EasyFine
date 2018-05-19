using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using PrivateForum.App.Web.Services.Models.Topic;
using PrivateForum.App.Web.Services.Repository.Abstraction;
using PrivateForum.Core;
using PrivateForum.Core.Framework.Security;

namespace PrivateForum.App.Web.Services.Repository.Implementation
{
    public class TopicRepository : ITopicRepository
    {
        private readonly DbConnection connection;
        private readonly IAuditContext audit;

        public TopicRepository(IConnectionFactory connection, IAuditContext auditContext)
        {
            this.connection = connection.Connection;
            this.audit = auditContext;
        }


        public async Task<int> Create(CreateTopicVM topic)
        {
            var parameters = new DynamicParameters(new { headId = topic.HeadId, folderId = topic.FolderId, header = topic.Header, subtitle = topic.Subtitle });

            return await connection.ExecuteScalarAsync<int>("sp_Forum_TopicAppRepository_CreateTopic", parameters.AuditInsert(audit), commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Delete(int id)
        {
            var parameters = new DynamicParameters(new { topicId = id });

            return await connection.ExecuteScalarAsync<bool>("sp_Forum_TopicAppRepository_DeleteTopic", parameters.AuditUpdate(audit), commandType: CommandType.StoredProcedure);
        }

        public async Task<TopicVM> GetById(int id)
        {
            var parameters = new DynamicParameters(new { topicId = id });
            var reader =  await connection.QueryMultipleAsync("sp_Forum_TopicAppRepository_GetTopicById", param: parameters, commandType: CommandType.StoredProcedure);

            var result = await reader.ReadFirstOrDefaultAsync<TopicVM>();
            result.Messages = await reader.ReadAsync<TopicMessageVM>();

            return result;
        }

        public async Task<bool> Update(EditTopicVM topic)
        {
            var parameters = new DynamicParameters(new { topicId = topic.Id, header = topic.Header, subtitle = topic.Subtitle });

            return await connection.ExecuteScalarAsync<bool>("sp_Forum_TopicAppRepository_EditTopic", parameters.AuditUpdate(audit), commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateTopicHeadId(int headId, int topicId)
        {
            var parameters = new DynamicParameters(new { headId = headId, topicId = topicId });

            return await connection.ExecuteScalarAsync<bool>("sp_Forum_TopicAppRepository_UpdateTopicHeadId", param: parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
