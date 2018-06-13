using PrivateForum.App.Web.Services.Models;
using PrivateForum.Apps.Services.Models.Identity;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Services.Abstraction
{
    public interface IUserService
    {
        Task<CurentUserVM> GetCurrentUserAsync();

        Task<UserProfileVM> GetUserProfile(int id);
        Task<bool> BlockUser(int id);
        Task<bool> DeleteUser(int id);
        Task<bool> UploadAvatarAsync(int id, string fileName, Stream file);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
