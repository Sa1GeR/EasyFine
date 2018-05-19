using PrivateForum.App.Web.Services.Models.Forum;
using PrivateForum.App.Web.Services.Repository.Abstraction;
using PrivateForum.App.Web.Services.Abstraction;
using PrivateForum.Core.Framework.Security;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Services.Implementation
{
    public class ForumService : IForumService
    {
        private readonly IForumRepository _forumRepository;
        private readonly IAuditContext _auditContext;

        public ForumService(
            IForumRepository forumRepository,
            IAuditContext auditContext
            )
        {
            this._forumRepository = forumRepository;
            this._auditContext = auditContext;
        }

        public async Task<bool> Create(CreateForumVM forum)
        {
            forum.UserId = int.Parse(_auditContext.Id);

            return await _forumRepository.Create(forum);
        }

        public async Task<bool> Delete(int id)
        {
            return await this._forumRepository.Delete(id);
        }

        public async Task<ForumVM> GetById(int id)
        {
            var userId = int.Parse(_auditContext.Id);
            return await this._forumRepository.GetById(id, userId);
        }

        public async Task<ForumVM> GetRootForum()
        {
            var userId = int.Parse(_auditContext.Id);
            return await this._forumRepository.GetRootForumAsync(userId);
        }

        public async Task<bool> Update(EditForumVM forum)
        {
            return await this._forumRepository.Update(forum);
        }
    }
}
