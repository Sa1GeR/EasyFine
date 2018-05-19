using PrivateForum.App.Web.Services.Models.Forum;
using PrivateForum.App.Web.Services.Models.Topic;
using PrivateForum.Core;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Services.Repository.Abstraction
{
    public interface ITopicRepository: IAppRepository
    {
        Task<int> Create(CreateTopicVM topic);
        Task<bool> Delete(int id);
        Task<bool> Update(EditTopicVM topic);
        Task<TopicVM> GetById(int id);
        Task<bool> UpdateTopicHeadId(int headId, int topicId);
    }
}
