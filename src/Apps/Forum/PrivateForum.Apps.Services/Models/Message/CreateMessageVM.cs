namespace PrivateForum.App.Web.Services.Models.Message
{
    public class CreateMessageVM
    {
        public int? ReplyId { get; set; }
        public string Content { get; set; }
        public int TopicId { get; set; }
        public int AuthorId { get; set; }
    }
}
