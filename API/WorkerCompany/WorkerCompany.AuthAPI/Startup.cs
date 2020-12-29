using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerCompany.Authentication.AuthItems;
using WorkerCompany.DAL.Models;
using Newtonsoft.Json;
using WorkerCompany.Authentication.Services.Abstraction;
using WorkerCompany.Authentication.Services.Implementation;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WorkerCompany.AuthAPI.Extensions;

namespace WorkerCompany.AuthAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersService();

            services.AddMvc();

            services.AddDbContextService(Configuration);

            services.AddIdentityService();

            var secret = Configuration["JwtSecret"];
            var issuer = Configuration["JwtIssuer"];
            var audience = Configuration["JwtAudience"];

            services = ConfigureJwt.Configure(services, secret, issuer, audience);

            services.AddCustomServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //var authSeeder = new AuthSeeder(app.ApplicationServices);
            //authSeeder.CreateAll();
        }
    }
}
