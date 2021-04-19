using ContactTracing15.Data;
using ContactTracing15.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Okta.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;

namespace ContactTracing15
{
    /// <summary>
    /// Class that is instantiated at runtime to initialise the application
    /// </summary>
    public class Startup
    {

        private string _googleApiKey = null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Main Database server context
            services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AppDB"));
            });
            // Identity Database server context
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // Add default identity service
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddRazorPages();

            //Add repository services (used for dependency injection)
            services.AddScoped<ICaseRepository, SQLCaseRepository>();
            services.AddScoped<IContactRepository, SQLContactRepository>();
            services.AddScoped<ITracerRepository, SQLTracerRepository>();
            services.AddScoped<ITesterRepository, SQLTesterRepository>();
            services.AddScoped<ITestingCentreRepository, SQLTestingCentreRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITracerService, TracerService>();
            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ITesterService, TesterService>();
            services.AddScoped<ITestingCentreService, TestingCentreService>();

            //Add Okta middleware configuration(Authentication)
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OktaDefaults.MvcAuthenticationScheme;
            }).AddCookie(options =>
            {
                options.AccessDeniedPath = "/Denied";
            }).AddOktaMvc(new OktaMvcOptions
            {
                // Replace these values with your Okta configuration
                OktaDomain = "https://dev-9464250.okta.com",
                ClientId = "0oajui6wuGYVC8WP95d6",
                ClientSecret = "7AeX4qoyVRn0cO4btEGGxV6XqWFzNWOnXyaFvPp8"
            });

            services.AddControllersWithViews();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("TracersOnly", policy => policy.Requirements.Add(new UserTypeRequirement("0")));
                options.AddPolicy("TestersOnly", policy => policy.Requirements.Add(new UserTypeRequirement("1")));
                options.AddPolicy("GovAgentOnly", policy => policy.Requirements.Add(new UserTypeRequirement("2")));
            });


            services.AddSingleton<IAuthorizationHandler, UserTypeHandler>();

            _googleApiKey = Configuration["googleApiKey"];

        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder object</param>
        /// <param name="env">IWebHostEnvironment object</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            app.Run(async (context) =>
            {
                var result = string.IsNullOrEmpty(_googleApiKey) ? "Null" : "Not Null";
                await context.Response.WriteAsync($"Secret is {result}");
            });
        }
    }
}
