using PrivateForum.Core.Framework.Security;
using PrivateForum.App.Web.Services.Models;
using PrivateForum.App.Web.Services.Repository.Abstraction;
using PrivateForum.App.Web.Services.Abstraction;
using System.Threading.Tasks;
using System.IO;
using PrivateForum.Core;
using System.Collections.Generic;

namespace PrivateForum.App.Web.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditContext _auditContext;
        private readonly string _storageUrl;

        public UserService(
            IUserRepository userRepository,
            IAuditContext auditContext
            )
        {
            this._userRepository = userRepository;
            this._auditContext = auditContext;

        }

        public async Task<bool> BlockUser(int id)
        {
            return await this._userRepository.BlockUser(id);
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await this._userRepository.DeleteUser(id);
        }

        public async Task<IEnumerable<UserProfileVM>> GetAllAsync()
        {
            return await this._userRepository.GetAllAsync();
        }

        public async Task<CurentUserVM> GetCurrentUserAsync()
        {
            var userId = int.Parse(_auditContext.Id);

            return await _userRepository.GetCurrentUserAsync(userId);
        }

        public async Task<UserProfileVM>  GetUserProfile(int id)
        {
            return await _userRepository.GetUserProfile(id);
        }

        public async Task<bool> UploadAvatarAsync(int id, string fileName, Stream file)
        {
            return await _userRepository.SaveUserAvatar(id, "");
        }
    }
}
