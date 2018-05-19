using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using PrivateForum.App.Web.Services.Models.Message;
using PrivateForum.App.Web.Services.Repository.Abstraction;
using PrivateForum.Core;
using PrivateForum.Core.Framework.Security;

namespace PrivateForum.App.Web.Services.Repository.Implementation
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DbConnection connection;
        private readonly IAuditContext audit;

        public MessageRepository(IConnectionFactory connection, IAuditContext auditContext)
        {
            this.connection = connection.Connection;
            this.audit = auditContext;
        }


        public async Task<int> Create(CreateMessageVM message)
        {
            var parameters = new DynamicParameters(new {  replyId = message.ReplyId, 
                                                          content = message.Content,
                                                          topicId = message.TopicId,
                                                          authorId = message.AuthorId
            });

            return await connection.ExecuteScalarAsync<int>("sp_Forum_MessageAppRepository_CreateMessage", parameters.AuditInsert(audit), commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Delete(int id)
        {
            var parameters = new DynamicParameters(new { messageId = id });

            return await connection.ExecuteScalarAsync<bool>("sp_Forum_MessageAppRepository_DeleteMessage", parameters.AuditUpdate(audit), commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> Update(EditMessageVM message)
        {
            var parameters = new DynamicParameters(new {  messageId = message.Id, content = message.Content });

            return await connection.ExecuteScalarAsync<bool>("sp_Forum_MessageAppRepository_EditMessage", parameters.AuditUpdate(audit), commandType: CommandType.StoredProcedure);
        }
    }
}
