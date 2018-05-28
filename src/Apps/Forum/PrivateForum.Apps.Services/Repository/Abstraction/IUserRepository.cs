using PrivateForum.Core;
using PrivateForum.App.Web.Services.Models;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace PrivateForum.App.Web.Services.Repository.Abstraction
{
    public interface IUserRepository : IAppRepository
    {
        Task<CurentUserVM> GetCurrentUserAsync(int userId);
        Task<UserProfileVM> GetUserProfile(int id);
        Task<bool> BlockUser(int id);
        Task<bool> DeleteUser(int id);
        Task<bool> SaveUserAvatar(int id, string url);
        Task<IEnumerable<UserProfileVM>> GetAllAsync();
    }
}
