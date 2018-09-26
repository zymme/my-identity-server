using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IdentityServerv2.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServerv2.Models;
using System.Reflection;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;

namespace IdentityServerv2
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment _env { get; set; }
        private ILoggerFactory _loggerFactory {get;set;}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _loggerFactory = new LoggerFactory();

            var cors = new DefaultCorsPolicyService(_loggerFactory.CreateLogger<DefaultCorsPolicyService>())
            {
                AllowedOrigins = { "http://localhost:4200" }
            };

            services.AddSingleton<ICorsPolicyService>(cors);

            
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;


            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
            
            //services.AddDefaultIdentity<IdentityUser>()
                //.AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddIdentityServer()
                    //.AddDeveloperSigningCredential()
                    .AddSigningCredential(Config.GetSigningCertificate(_env.ContentRootPath))
                   
                    // this adds config data from DB (clients, resources)
                    .AddConfigurationStore(options => {

                        options.ConfigureDbContext = builder =>
                            builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                                                 sql => sql.MigrationsAssembly(migrationsAssembly));
            
                    })

                    // this adds the operational data from DB (codes, tokens, consents)
                    .AddOperationalStore(options => {
                        options.ConfigureDbContext = builder =>
                            builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                                                 sql => sql.MigrationsAssembly(migrationsAssembly));
                
                    })

                    //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                    //.AddInMemoryApiResources(Config.GetApiResources())
                    //.AddInMemoryClients(Config.GetClients())
                    .AddAspNetIdentity<ApplicationUser>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //app.UseAuthentication();
            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
