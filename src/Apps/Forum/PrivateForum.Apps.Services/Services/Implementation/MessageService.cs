using PrivateForum.App.Web.Services.Models.Forum;
using PrivateForum.App.Web.Services.Repository.Abstraction;
using PrivateForum.App.Web.Services.Abstraction;
using PrivateForum.Core.Framework.Security;
using System.Threading.Tasks;
using PrivateForum.App.Web.Services.Models.Message;

namespace PrivateForum.App.Web.Services.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IAuditContext _auditContext;

        public MessageService(
            IMessageRepository messageRepository,
            IAuditContext auditContext
            )
        {
            this._messageRepository = messageRepository;
            this._auditContext = auditContext;
        }

        public async Task<int> Create(CreateMessageVM message)
        {
            return await _messageRepository.Create(message);
        }

        public async Task<bool> Delete(int id)
        {
            return await this._messageRepository.Delete(id);
        }

        public async Task<bool> Update(EditMessageVM message)
        {
            return await this._messageRepository.Update(message);
        }
    }
}
