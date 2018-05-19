using System.Collections.Generic;

namespace PrivateForum.App.Web.Services.Models.Forum
{
    public class ForumVM : BaseForumVM
    {
        public IEnumerable<BaseForumVM> SubForums { get; set; }
        public IEnumerable<ForumTopicVM> Topics { get; set; }
    }
}
