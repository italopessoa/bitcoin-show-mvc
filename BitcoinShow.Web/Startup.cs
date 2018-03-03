using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BitcoinShow.Web.Facade;
using BitcoinShow.Web.Facade.Interface;
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
        private readonly Container container = new Container();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("enable-cors", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddMvc();
            Mapper.Initialize(cfg => {
                cfg.AddProfile<BitcoinShowProfile>();
            });
                        services.AddMvc().AddJsonOptions(options =>
            {
            #if DEBUG
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            #endif
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });
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
                GenerateMockQuestions();
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
            container.Register<IQuestionRepository, QuestionRepository>(Lifestyle.Scoped);
            container.Register<IOptionRepository, OptionRepository>(Lifestyle.Scoped);
            container.Register<IAwardRepository, AwardRepository>(Lifestyle.Scoped);
            container.Register<IQuestionService, QuestionService>(Lifestyle.Scoped);
            container.Register<IOptionService, OptionService>(Lifestyle.Scoped);
            container.Register<IAwardService, AwardService>(Lifestyle.Scoped);
            container.Register<IBitcoinShowFacade, BitcoinShowFacade>(Lifestyle.Scoped);

            container.Register<BitcoinShowDBContext>(() =>
            {
                var cs = Configuration.GetConnectionString("SqlServer");
                var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
                    .UseSqlServer(cs)
                    .Options;

                var context = new BitcoinShowDBContext(options);
                context.Database.Migrate();
                context.Database.EnsureCreated();
                return context;
            }, Lifestyle.Scoped);

            //Cross-wire ASP.NET services (if any). For instance:
            container.CrossWire<ILoggerFactory>(app);

            container.Verify();

            // NOTE: Do prevent cross-wired instances as much as possible.
            // See: https://simpleinjector.org/blog/2016/07/
        }
        private void teste(BitcoinShowDBContext context, LevelEnum level, int quantidade)
        {
            for (int i = 0; i < quantidade; i++)
            {
                Question easyQuestion = new Question();
                easyQuestion.Title = $"{level.GetEnumDisplayName()} Question {i}";
                easyQuestion.Level = level;
                List<Option> optionsList = new List<Option>
                        {
                            new Option{Text = $"{level.GetEnumDisplayName()} Question {i} option A"},
                            new Option{Text = $"{level.GetEnumDisplayName()} Question {i} option B"},
                            new Option{Text = $"{level.GetEnumDisplayName()} Question {i} option C"},
                            new Option{Text = $"{level.GetEnumDisplayName()} Question {i} option D"},
                        };
                optionsList.ForEach(o =>
                {
                    context.Options.Add(o);
                });
                context.SaveChanges();

                easyQuestion.Answer = optionsList[0];
                context.Questions.Add(easyQuestion);

                easyQuestion.Options = optionsList;
                optionsList.ForEach(o =>
                {
                    o.Question = easyQuestion;
                    context.Options.Update(o);
                });
                context.SaveChanges();
            }
        }
        private void GenerateMockQuestions()
        {
            var cs = Configuration.GetConnectionString("SqlServer");
            var options = new DbContextOptionsBuilder<BitcoinShowDBContext>()
                .UseSqlServer(cs)
                .Options;

            using (var context = new BitcoinShowDBContext(options))
            {
                context.Database.Migrate();
                context.Database.EnsureCreated();

                context.Options.ToList().ForEach(o =>
                {
                    o.Question = null;
                    o.QuestionId = null;
                    context.Options.Update(o);
                });
                context.SaveChanges();
                context.Questions.RemoveRange(context.Questions);
                context.Options.RemoveRange(context.Options);
                context.SaveChanges();

                teste(context, LevelEnum.Easy, 2);
                teste(context, LevelEnum.Medium, 2);
                teste(context, LevelEnum.Hard, 2);
                teste(context, LevelEnum.VeryHard, 2);
            };
        }
    }
}
