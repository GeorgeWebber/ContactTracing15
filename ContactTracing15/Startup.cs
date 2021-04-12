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

namespace ContactTracing15
{
    /// <summary>
    /// Class that is instantiated at runtime to initialise the application
    /// </summary>
    public class Startup
    {
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

            //Add Okta middleware configuration(Authentication)
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OktaDefaults.MvcAuthenticationScheme;
            })
            .AddCookie()
            .AddOktaMvc(new OktaMvcOptions
            {
                // Replace these values with your Okta configuration
                OktaDomain = "https://dev-9464250.okta.com",
                ClientId = "0oajui6wuGYVC8WP95d6",
                ClientSecret = "7AeX4qoyVRn0cO4btEGGxV6XqWFzNWOnXyaFvPp8"
            });

            services.AddControllersWithViews();
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
        }
    }
}
