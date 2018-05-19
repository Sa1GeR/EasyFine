using Autofac;
using PrivateForum.Core.Framework.Commanding;

namespace PrivateForum.Core
{
    public class Registry : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region Services

            builder.RegisterType<CommandBus>().As<ICommandBus>();
            #endregion
            
            base.Load(builder);
        }
    }
}
