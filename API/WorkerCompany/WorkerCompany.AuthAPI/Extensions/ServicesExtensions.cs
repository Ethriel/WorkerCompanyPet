using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WorkerCompany.Authentication.AuthItems;
using WorkerCompany.Authentication.Services.Abstraction;
using WorkerCompany.Authentication.Services.Implementation;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.AuthAPI.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddControllersService(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddNewtonsoftJson(options => options.SerializerSettings
                                                         .ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            return services;
        }

        public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services = ConfigureJwt.Configure(services, configuration);

            return services;
        }

        public static IServiceCollection AddAspNetCoreIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<WorkerCompanyPetContext>()
                    .AddRoles<IdentityRole>()
                    .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddDbContextService(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<WorkerCompanyPetContext>(options => options.UseLazyLoadingProxies()
                                                                             .UseSqlServer(configuration.GetConnectionString("Default")));
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IGenerateJwt, GenerateJwt>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
