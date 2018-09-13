using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MyIdentityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var cert = new System.Security.Cryptography.X509Certificates.X509Certificate2("wildcard.viawest.net.pfx", "insecure");


            services.AddIdentityServer()
                    .AddSigningCredential(cert)
                    .AddSamlPlugin(options =>
                     {
                         options.WantAuthenticationRequestsSigned = false;
                         options.LicenseKey = "REVNT3x8fDI3LzA5LzIwMTh8TWZCR3FWVGxpRmhnQUpuOElrWVRKSzZBQjVNMTc1Y2kvT2ZGMWd4U21VaHYyVGVibWJWNFZ5Mmt4Z0g1VE1jd25WeXg1eGdLWTl0bkVzWElocmpMcTF3SDNPbjhCRkQzVURWcDJmb05vVUpnWmlTK3ZDT2pUYml4d2JlSUpjRjdXVzBTSmZPenFMdVRjaU5MZTJiaEZLTzJDbEQwMzZWS2VVcUVsS1lNWE13U1djN0RmSm1BR0tGaDNDVGd3R0VVUWE2aUZPRmtlamtiaUg1SnBsUUpwem9Qa1BsbmRDUHNZTjRSREhlUStYSUFUdGU4NFJML3hFT1hYbWYvVTMrNkNNaDk1U29oTWw2Ump4eXJpcEhERjNlL212V1BGRUlreC9EOHpuNVFKK2RIVElBdjZwaHIrUEFJcDF3ajJGR2VPZjNRK0RyU0VKQWJWMUhValkvSldwc0IyVVB0ZDF5UGdPQ0lZeFFlVGRmc2cyYzdXUXIrVEg1TFAvanZxMWFCMXlXMjhtb0h6Ym9xUDdGcE5ZNGZyZDk2YUQ4Q0QvZjlmYUNlK0wzTFlNRGgxbnVXK295R0FINVgvcjlxcnZ2dmZFeGwwbDNiL0tJdy92WkFHRkhoYSsrbDgwYTMzYjkxd0R4ZnBzNTlRam10TTVsQ0tOd3d3QTlKejZqQ2RYYjdtZHVlNnZNdVZLeFNZNml1WnkzUlpjSFl1ckF6NXpkaFFlYXVobUFOd0tGb2phdm11RVJmYTVjVzFDQnZPZkRRSG10UnhUcEMxTTJzNGlxOGdTUzY4Q1hzbW9aUWc2OFBhREhDdTExR2M5Z3dNR2h0S1ZIWVNxZ2l3aEprS0xDUG1DRm9KaUhMdDdYQS82M2o5ZVc1YUtBL0llcndYUGlwOUNPRFhKQnpOWG89";
                         options.Licensee = "Demo";


                     })
                    .AddInMemoryIdentityResources(Config.GetIdentityResources())
                    .AddInMemoryApiResources(Config.GetApiResources())
                    .AddInMemoryClients(Config.GetClients())
                    .AddInMemoryServiceProviders(Config.GetServiceProviders())
                    .AddTestUsers(Config.GetUsers());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer()
               .UseIdentityServerSamlPlugin();

            app.UseMvcWithDefaultRoute();

           
        }
    }
}
