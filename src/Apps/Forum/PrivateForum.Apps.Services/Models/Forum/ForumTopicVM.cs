namespace PrivateForum.App.Web.Services.Models.Forum
{
    public class ForumTopicVM : BaseModelVM
    {
        public int? HeadId { get; set; }
        public int FolderId { get; set; }
        public string Header { get; set; }
        public string Subtitle { get; set; }
    }
}
