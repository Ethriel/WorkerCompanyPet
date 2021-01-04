using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.Authentication.AuthItems
{
    public class AuthSeeder
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;
        private readonly WorkerCompanyPetContext context;
        private readonly IServiceScope scope;

        public AuthSeeder()
        {

        }

        public AuthSeeder(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, WorkerCompanyPetContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
        }
        public AuthSeeder(IServiceProvider services)
        {
            //using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    this.roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //    this.userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            //}
            this.scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            this.roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            this.userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            this.context = scope.ServiceProvider.GetRequiredService<WorkerCompanyPetContext>();
        }

        public void CreateAll()
        {
            CreateRoles();
            CreateUsers();
            this.scope.Dispose();
        }

        public void CreateRoles()
        {
            CreateRole(AuthRoles.Admin);
            CreateRole(AuthRoles.User);
            CreateRole(AuthRoles.Manager);
        }

        public void CreateUsers()
        {
            CreateUser("App admin", "admin@gmail.com", "Qwerty-1", AuthRoles.Admin, 1);
            CreateUser("App test user", "testuser@gmail.com", "Qwerty-1", AuthRoles.User, 2);
            CreateUser("App test manager", "testmanager@gmail.com", "Qwerty-1", AuthRoles.Manager, 3);
        }

        private void CreateRole(string name)
        {
            var existingRole = roleManager.Roles.Any(r => r.Name.Equals(name));

            if (!existingRole)
            {
                var role = new IdentityRole
                {
                    Name = name,
                    NormalizedName = name.ToUpper()
                };
                var result = roleManager.CreateAsync(role).Result;
            }
        }

        private void CreateUser(string displayName, string email, string password, string role, int workerId)
        {
            var existingUser = userManager.Users.Any(u => u.Email.Equals(email));

            if (!existingUser)
            {
                var worker = context.Worker.FirstOrDefault(w => w.Id.Equals(workerId));
                var user = new AppUser
                {
                    DisplayName = displayName,
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true,
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    WorkerId = workerId
                };
                var result = userManager.CreateAsync(user, password).Result;
                worker.AppUserId = user.Id;
                context.SaveChanges();
                AttachUserToRole(user, role);
            }
        }

        private void AttachUserToRole(AppUser user, string role)
        {
            var userRoles = userManager.GetRolesAsync(user).Result;

            var existingRole = userRoles.Any(r => r.Equals(role));

            if (!existingRole)
            {
                var result = userManager.AddToRoleAsync(user, role).Result;
            }
        }
    }
}
