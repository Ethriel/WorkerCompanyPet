using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WorkerCompany.BLL.Helpers;
using WorkerCompany.BLL.Services.Abstraction;
using WorkerCompany.BLL.Services.Implementation;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.API.Extensions
{
    public static class ServicesExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                    .AddNewtonsoftJson(options => options.SerializerSettings
                                                         .ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            AddCustomServices(services);

            services.AddDbContext<WorkerCompanyPetContext>(options =>
                                                           options.UseLazyLoadingProxies()
                                                                  .UseSqlServer(configuration.GetConnectionString("Default")));
        }

        private static void AddCustomServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IServerService), typeof(ServerService));
            services.AddAutoMapper(AllMapperProfiles.Profiles);
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped(typeof(IGenericEntityService<>), typeof(GenericEntityService<>));
            services.AddScoped(typeof(IMapperService<,>), typeof(MapperService<,>));
            services.AddScoped(typeof(ICRUDService<,>), typeof(CRUDService<,>));
        }
    }
}
