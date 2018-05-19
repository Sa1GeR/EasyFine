namespace PrivateForum.App.Web.Services.Models.Forum
{
    public class CreateForumVM
    {
        public string Name { get; set; }
        public int ParentId { get; set; }
        public int UserId { get; set; }
    }
}
