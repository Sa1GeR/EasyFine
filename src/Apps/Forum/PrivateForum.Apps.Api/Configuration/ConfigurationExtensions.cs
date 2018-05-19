using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateForum.App.Web.Configuration
{
    public static class ConfigurationExtensions
    {
        public static T Resolve<T>(this IServiceCollection collection)
        {
            return collection.ServiceProvider().GetService<T>();
        }

        private static IServiceProvider ServiceProvider(this IServiceCollection services)
        {
            return services.BuildServiceProvider();
        }
    }
}
