using Autofac;
using PrivateForum.App.Web.Services.Implementation;
using PrivateForum.App.Web.Services.Abstraction;
using PrivateForum.App.Web.Services.Repository.Implementation;
using PrivateForum.App.Web.Services.Repository.Abstraction;

namespace PrivateForum.App.Web
{
    public class Registry : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Services

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<ForumService>().As<IForumService>();
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<TopicService>().As<ITopicService>();


            #endregion

            #region AdminRepositories

            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<ForumRepository>().As<IForumRepository>();
            builder.RegisterType<MessageRepository> ().As<IMessageRepository>();
            builder.RegisterType<TopicRepository>().As<ITopicRepository>();


            #endregion

            base.Load(builder);
        }
    }
}
