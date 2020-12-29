using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;
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
            var secret = configuration["JwtSecret"];

            var key = Encoding.ASCII.GetBytes(secret);
            var symmetricKey = new SymmetricSecurityKey(key);
            var issuer = configuration["JwtIssuer"];
            var audience = configuration["JwtAudience"];
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = symmetricKey,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

        public static IServiceCollection AddIdentityService(this IServiceCollection services)
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

            return services;
        }
    }
}
