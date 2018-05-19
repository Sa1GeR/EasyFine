namespace PrivateForum.App.Web.Services.Models.Topic
{
    public class CreateTopicVM
    {
        public int? HeadId { get; set; }
        public int FolderId { get; set; }
        public string Header { get; set; }
        public string Subtitle { get; set; }
        public TopicMessageVM Message { get; set; }
    }
}
