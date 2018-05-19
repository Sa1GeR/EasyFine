using PrivateForum.App.Web.Services.Models.Message;
using PrivateForum.Core;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Services.Repository.Abstraction
{
    public interface IMessageRepository : IAppRepository
    {
        Task<int> Create(CreateMessageVM message);
        Task<bool> Delete(int id);
        Task<bool> Update(EditMessageVM message);
    }
}
