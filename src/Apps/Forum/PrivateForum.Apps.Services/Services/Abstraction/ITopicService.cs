using PrivateForum.App.Web.Services.Models.Forum;
using PrivateForum.App.Web.Services.Models.Topic;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Services.Abstraction
{
    public interface ITopicService
    {
        Task<bool> Create(CreateTopicVM topic);
        Task<bool> Delete(int id);
        Task<bool> Update(EditTopicVM topic);
        Task<TopicVM> GetById(int id);
    }
}
