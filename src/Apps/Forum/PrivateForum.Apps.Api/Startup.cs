using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PrivateForum.App.Web.Configuration.Security;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using PrivateForum.Core.Framework.Security;
using PrivateForum.Core;
using PrivateForum.Core.Framework.Commanding;
using PrivateForum.App.Web.Configuration;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.AspNetCore.Rewrite;

namespace PrivateForum.App.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; private set; }
        public IHostingEnvironment Environment { get; private set; }
        public IContainer ApplicationContainer { get; private set; }

        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(environment.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();

            builder.AddInMemoryCollection();
            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<DbConfiguration>(Configuration.GetSection("DatabaseConfiguration"));

            services.ConfigureForumAuthorization();
            services.AddMvc();

            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterType<AuditContext>().As<IAuditContext>();
            builder.RegisterType<CommandConnection>().As<IConnectionFactory>();
            builder.Register<IDomainRepositoryFactory>(c => new DomainRepositoryFactory(this.ApplicationContainer));
            builder.Register<IConfiguratorFactory>(c => new ConfiguratorFactory(this.ApplicationContainer));

            builder.RegisterModule(new PrivateForum.App.Web.Registry());
            builder.RegisterModule(new PrivateForum.Core.Registry());
            this.ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            

            //if (Environment.IsEnvironment("development") == false)
            //    app.UseForceHttpsMiddleware();

            app.UseAuthentication();

            //app.UseAppProtectionMiddleware();

            app.UseMvc();

            app.Use(async (context, next) => {
                if (!context.Request.Path.StartsWithSegments("/api") && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/";
                }
                await next();
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
