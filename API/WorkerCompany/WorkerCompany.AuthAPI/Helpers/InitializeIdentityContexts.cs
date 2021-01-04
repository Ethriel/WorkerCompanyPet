using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace WorkerCompany.AuthAPI.Helpers
{
    public static class InitializeIdentityContexts
    {
        public static object IdentityServerConfiguration { get; private set; }

        public static void Init(IServiceProvider services)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                //scopeServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                //scopeServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                foreach (var client in IdentityServerConfig.Clients)
                {
                    if (!context.Clients.Any(c => c.ClientId == client.ClientId))
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                }
                context.SaveChanges();

                foreach (var resource in IdentityServerConfig.IdentityResources)
                {
                    if (!context.IdentityResources.Any(r => r.Name == resource.Name))
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                }
                context.SaveChanges();

                foreach (var apiScope in IdentityServerConfig.ApiScopes)
                {
                    if (!context.ApiScopes.Any(asc => asc.Name == apiScope.Name))
                    {
                        context.ApiScopes.Add(apiScope.ToEntity());
                    }
                }
                context.SaveChanges();

                //if (!context.ApiResources.Any())
                //{
                //    foreach (var resource in Config.ApiScopes)
                //    {
                //        context.ApiResources.Add(resource.ToEntity());
                //    }
                //    context.SaveChanges();
                //}



                //var manager = scope.ServiceProvider.GetRequiredService<UserManager<DbUser>>();
                //var managerRole = scope.ServiceProvider.GetRequiredService<RoleManager<DbRole>>();
                //SeederDB.SeedData(manager, managerRole);
            }
        }
    }
}
