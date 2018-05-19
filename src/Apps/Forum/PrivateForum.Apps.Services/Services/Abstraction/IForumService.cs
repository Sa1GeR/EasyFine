using PrivateForum.App.Web.Services.Models.Forum;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Services.Abstraction
{
    public interface IForumService
    {
        Task<bool> Create(CreateForumVM forum);
        Task<bool> Delete(int id);
        Task<bool> Update(EditForumVM forum);
        Task<ForumVM> GetById(int id);
        Task<ForumVM> GetRootForum();
    }
}
