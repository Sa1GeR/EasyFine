using PrivateForum.App.Web.Services.Models.Message;
using PrivateForum.App.Web.Services.Models.Topic;

namespace PrivateForum.App.Web.Services
{
    public static class Extensions
    {
        public static CreateMessageVM ToCreateMessageModel(this TopicMessageVM message)
        {
            return new CreateMessageVM
            {
                 Content = message.Content,
                 ReplyId = message.ReplyId,
                 AuthorId = message.AuthorId,
                 TopicId = message.TopicId.Value
            };
        }
    }
}
