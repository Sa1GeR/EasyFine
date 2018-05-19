namespace PrivateForum.App.Web.Services.Models.Forum
{
    public class BaseForumVM : BaseModelVM
    {
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int CourseId { get; set; }
    }
}
