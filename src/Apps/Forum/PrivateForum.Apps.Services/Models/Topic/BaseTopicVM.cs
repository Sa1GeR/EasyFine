using System;
using System.Collections.Generic;
using System.Text;

namespace PrivateForum.App.Web.Services.Models.Topic
{
    public class BaseTopicVM : BaseModelVM
    {
        public int HeadId { get; set; }
        public int FolderId { get; set; }
        public string Header { get; set; }
        public string Subtitle { get; set; }
    }
}
