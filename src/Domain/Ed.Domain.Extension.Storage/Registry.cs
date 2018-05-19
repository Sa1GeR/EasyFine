using Autofac;
using PrivateForum.Core;
using Microsoft.WindowsAzure.Storage;

namespace PrivateForum.Domain.Extension.Storage
{
    public class Registry : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AzureStorageProvider>().As<IStorageProvider>();
            builder.RegisterType<AzureStorageProvider>().As<IQueueProvider>();
            base.Load(builder);
        }
    }
}
