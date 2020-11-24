using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WorkerCompany.BLL.Helpers;
using WorkerCompany.BLL.Services.Abstraction;
using WorkerCompany.BLL.Services.Implementation;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.UnitTests.Helpers
{
    public class TestServices
    {
        public static IServiceProvider BuildServiceProvider(string dbName)
        {
            var services = new ServiceCollection();
            services.AddDbContext<WorkerCompanyPetContext>(options =>
                                                           options.UseLazyLoadingProxies()
                                                                  .UseInMemoryDatabase(databaseName: dbName));
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddAutoMapper(AllMapperProfiles.Profiles);
            services.AddScoped(typeof(IGenericEntityService<>), typeof(GenericEntityService<>));
            services.AddScoped(typeof(IMapperService<,>), typeof(MapperService<,>));
            services.AddScoped(typeof(ICRUDService<,>), typeof(CRUDService<,>));

            return services.BuildServiceProvider();
        }
    }
}
