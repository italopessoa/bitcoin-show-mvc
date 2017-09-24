using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BitcoinShow.Web.Models;
using BitcoinShow.Web.Repositories;
using BitcoinShow.Web.Repositories.Interface;
using BitcoinShow.Web.Services;
using BitcoinShow.Web.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace BitcoinShow.Web
{
    public class Startup
    {
        private Container container = new Container();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            IntegrateSimpleInjector(services);
        }

        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(container));

            services.EnableSimpleInjectorCrossWiring(container);
            services.UseSimpleInjectorAspNetRequestScoping(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory factory)
        {
            InitializeContainer(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        
        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            container.RegisterMvcControllers(app);
            container.RegisterMvcViewComponents(app);

            // Add application services. For instance:
            container.Register<IQuestionService, QuestionService>(Lifestyle.Scoped);
            container.Register<IQuestionRepository, QuestionRepository>(Lifestyle.Scoped);

            container.Register<BitcoinShowDBContext>(() => {
                var cs = Configuration.GetConnectionString("SqlServer");
                var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
                    .UseSqlServer(cs)
                    .Options;
                
                var context = new BitcoinShowDBContext(options);
                context.Database.Migrate();
                context.Database.EnsureCreated();
                return context;
            },Lifestyle.Scoped);

            //Cross-wire ASP.NET services (if any). For instance:
            container.CrossWire<ILoggerFactory>(app);
            
            container.Verify();

            // NOTE: Do prevent cross-wired instances as much as possible.
            // See: https://simpleinjector.org/blog/2016/07/
        }
    }
}