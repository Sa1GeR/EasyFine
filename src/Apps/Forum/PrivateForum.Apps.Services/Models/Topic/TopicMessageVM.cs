namespace PrivateForum.App.Web.Services.Models.Topic
{
    public class TopicMessageVM : BaseModelVM
    {
        public int? ReplyId { get; set; }
        public int? TopicId { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public string AvatarUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
