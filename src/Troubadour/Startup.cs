using Microsoft.AspNet.Builder;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Troubadour.Models;
using Troubadour.Services;
using Microsoft.Dnx.Runtime;
using Microsoft.AspNet.Diagnostics;
using Microsoft.Framework.Logging;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Troubadour.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authentication.Cookies;
using System.Threading.Tasks;
using System.Net;

namespace Troubadour
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IApplicationEnvironment applicationEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(applicationEnvironment.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(configuration =>
            {
#if !DEBUG
                    configuration.Filters.Add(new RequireHttpsAttribute());
#endif
            })
            .AddJsonOptions(o =>
            {
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            Mapper.Initialize(configuration =>
            {
                configuration.CreateMap<Story, StoryViewModel>().ReverseMap();
                configuration.CreateMap<Response, ResponseViewModel>().ReverseMap();
                configuration.CreateMap<Tag, TagViewModel>().ReverseMap();
                configuration.CreateMap<Story, Story>();
            });

            services.AddIdentity<TroubadourUser, IdentityRole>(configuration =>
            {
                configuration.User.RequireUniqueEmail = true;
                configuration.Password.RequiredLength = 6;
                configuration.Password.RequireDigit = false;
                configuration.Password.RequireNonLetterOrDigit = false;
                configuration.Cookies.ApplicationCookie.LoginPath = "/Auth/Login";
                configuration.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirect = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }

                        return Task.FromResult(0);
                    }
                };
            })
            .AddEntityFrameworkStores<TroubadourContext>();

            services.AddLogging();

#if DEBUG
            services.AddScoped<IMailService, DebugMailService>();
#else

#endif

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<TroubadourContext>();

            services.AddTransient<TroubadourContextSeedData>();

            services.AddScoped<IStoryRepository, StoryRepository>();
            services.AddScoped<IResponseRepository, ResponseRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public async void Configure(IApplicationBuilder app, TroubadourContextSeedData seeder, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug(LogLevel.Information);

            app.UseDeveloperExceptionPage();
            app.UseIdentity();
            app.UseStaticFiles();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Blog", action = "Index" }
                    );
            });

            await seeder.PerformSeedDataAsync();
        }
    }
}
