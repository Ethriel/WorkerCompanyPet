using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using WorkerCompany.Authentication.AuthItems;

namespace WorkerCompany.Gateway
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
            services = ConfigureJwt.Configure(services, Configuration);

            services.AddCors(cors =>
            {
                cors.AddPolicy("CorsPolicy", builder => builder.WithOrigins(Configuration["ClientApp"])
                                                               .AllowAnyMethod()
                                                               .AllowAnyHeader()
                                                               .AllowCredentials());
            });

            services.AddOcelot()
                    .AddCacheManager(settings => settings.WithDictionaryHandle());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthentication()
               .UseOcelot()
               .Wait();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Gateway works");
                });
            });
        }
    }
}
