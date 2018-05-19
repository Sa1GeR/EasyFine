using PrivateForum.App.Web.Services.Models.Forum;
using PrivateForum.Core;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Services.Repository.Abstraction
{
    public interface IForumRepository: IAppRepository
    {
        Task<bool> Create(CreateForumVM forum);
        Task<bool> Delete(int id);
        Task<bool> Update(EditForumVM forum);
        Task<ForumVM> GetById(int id, int userId);
        Task<ForumVM> GetRootForumAsync(int userId);
    }
}
