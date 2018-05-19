using System;
using System.Collections.Generic;
using System.Text;

namespace PrivateForum.App.Web.Services.Models.Topic
{
    public class EditTopicVM
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Subtitle { get; set; }
    }
}
