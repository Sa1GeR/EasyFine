using PrivateForum.App.Web.Services.Models.Forum;
using PrivateForum.App.Web.Services.Models.Message;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Services.Abstraction
{
    public interface IMessageService
    {
        Task<int> Create(CreateMessageVM forum);
        Task<bool> Delete(int id);
        Task<bool> Update(EditMessageVM message);
    }
}
