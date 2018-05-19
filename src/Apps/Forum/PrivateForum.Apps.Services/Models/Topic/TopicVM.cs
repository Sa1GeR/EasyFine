using System;
using System.Collections.Generic;
using System.Text;

namespace PrivateForum.App.Web.Services.Models.Topic
{
    public class TopicVM : BaseTopicVM
    {
        public IEnumerable<TopicMessageVM> Messages { get; set; }
    }
}
