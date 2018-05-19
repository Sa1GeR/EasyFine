using PrivateForum.App.Web.Services.Models.Forum;
using PrivateForum.App.Web.Services.Repository.Abstraction;
using PrivateForum.App.Web.Services.Abstraction;
using PrivateForum.Core.Framework.Security;
using System.Threading.Tasks;
using PrivateForum.App.Web.Services.Models.Topic;

namespace PrivateForum.App.Web.Services.Implementation
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMessageRepository _messageRepository;

        private readonly IAuditContext _auditContext;

        public TopicService(
            ITopicRepository topicRepository,
            IAuditContext auditContext,
            IMessageRepository messageRepository
            )
        {
            this._topicRepository = topicRepository;
            this._auditContext = auditContext;
            this._messageRepository = messageRepository;
        }

        public async Task<bool> Create(CreateTopicVM topic)
        {
            var topicId = await _topicRepository.Create(topic);
            topic.Message.TopicId = topicId;

            var messageId = await _messageRepository.Create(topic.Message.ToCreateMessageModel());

            return await _topicRepository.UpdateTopicHeadId(messageId, topicId);
        }

        public async Task<bool> Delete(int id)
        {
            return await this._topicRepository.Delete(id);
        }

        public async Task<TopicVM> GetById(int id)
        {
            return await this._topicRepository.GetById(id);
        }

        public async Task<bool> Update(EditTopicVM topic)
        {
            return await this._topicRepository.Update(topic);
        }
    }
}
