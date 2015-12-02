using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CRM.Models;
using CRM.Models.Helper;
using Microsoft.AspNet.Authorization;
using CRM.Services;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CRM
{
    public class Startup
    {
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // Add Entity Framework services to the services container.
            //services.AddEntityFramework()
            //    .AddSqlServer()
            //    .AddDbContext<ApplicationDbContext>(options =>
            //        options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
            //services.Configure<IdentityDbContextOptions>(options =>
            //{
            //    options.DefaultAdminUserName = Configuration["DefaultAdminUsername"];
            //    options.DefaultAdminPassword = Configuration["DefaultAdminPassword"];
            //});

            //// Add Identity services to the services container.
            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            
           
            // Add MVC services to the services container.
            services.AddMvc();


            var policy = new AuthorizationPolicyBuilder()
                //This is what makes it function like the basic [Authorize] attribute
                .RequireAuthenticatedUser()
                //add functionality similar to [Authorize(Roles="myrole")]
                .RequireRole("Administrator")
                //add functionality similar to [ClaimsAuthorize("myclaim")]
                //.RequireClaim("myclaim")
                .Build();

            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();

            // Register application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {


            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            //app.UseStaticFiles(); --migration
            
            // Add and configure the options for authentication middleware to the request pipeline.
            // You can add options for middleware as shown below.
            // For more information see http://go.microsoft.com/fwlink/?LinkID=532715
            //app.UseFacebookAuthentication(options =>
            //{
            //    options.AppId = Configuration["Authentication:Facebook:AppId"];
            //    options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //});

            //app.UseGoogleAuthentication(options =>
            //{
            //    options.ClientId = Configuration["Authentication:Google:ClientId"];
            //    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //});

            //app.UseMicrosoftAccountAuthentication(options =>
            //{
            //    options.ClientId = Configuration["Authentication:MicrosoftAccount:ClientId"];
            //    options.ClientSecret = Configuration["Authentication:MicrosoftAccount:ClientSecret"];
            //});

            //app.UseTwitterAuthentication(options =>
            //{
            //    options.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
            //    options.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
            //});

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
            //Populates the Admin user and role
            SampleData.InitializeIdentityDatabaseAsync(app.ApplicationServices).Wait();
        }

        public AppSettings getAplicationSettings()
        {
            AppSettings appSettings = new AppSettings()
            {
                SiteTitle = Configuration["AppSettings:SiteTitle"],
                themeOptions = new ThemeOptions()
                {
                    themeName = Configuration["AppSettings:ThemeOptions:ThemeName"],
                    font = Configuration["AppSettings:ThemeOptions:Font"],
                },

                defaultAdminUsername = Configuration["AppSettings:DefaultAdminUsername"],
                defaultAdminPassword = Configuration["AppSettings:DefaultAdminPassword"],

                signalRConfig = new SignalR_Config()
                {
                    server_Url = Configuration["AppSettings:SignalR_Config:Server_Url"],
                    javaScript_Url = Configuration["AppSettings:SignalR_Config:JavaScript_Url"],
                },

                sendGridConfig = new SendGrid_Config()
                {
                    userName = Configuration["AppSettings:SendGrid_Config:UserName"],
                    password = Configuration["AppSettings:SendGrid_Config:Password"],
                    templateId = Configuration["AppSettings:SendGrid_Config:TemplateId"],

                },

                primaveraConfig = new Primavera_Config()
                {
                    userName = Configuration["AppSettings:Primavera_Config:UserName"],
                    password = Configuration["AppSettings:Primavera_Config:Password"],
                    instancia = Convert.ToInt16(Configuration["AppSettings:Primavera_Config:Instancia"])

                }
            };

            return appSettings;
        }
    }
}
