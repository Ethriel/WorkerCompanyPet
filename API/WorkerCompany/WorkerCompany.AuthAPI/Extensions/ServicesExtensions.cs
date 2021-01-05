using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
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

        public static IServiceCollection AddIdentityServerService(this IServiceCollection services, IConfiguration configuration)
        {
            var persistantGrantString = configuration.GetConnectionString("PersistantGrantConnection");
            var configurationDbString = configuration.GetConnectionString("ConfigurationDbConnection");

            var migrationsAssembly = typeof(WorkerCompanyPetContext).GetTypeInfo().Assembly.GetName().Name;

            var builder = services.AddIdentityServer(options =>
            {
                //options.Events.RaiseErrorEvents = true;
                //options.Events.RaiseInformationEvents = true;
                //options.Events.RaiseFailureEvents = true;
                //options.Events.RaiseSuccessEvents = true;
                //options.UserInteraction.LoginUrl = "/Auth/sign-in";
                //options.UserInteraction.LogoutUrl = "/Auth/sign-out";
            })
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(persistantGrantString,
                sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = b => b.UseSqlServer(configurationDbString,
                sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddAspNetIdentity<AppUser>()
            .AddProfileService<ProfileService>();

            builder.AddDeveloperSigningCredential();

            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            return services;
        }
    }
}
