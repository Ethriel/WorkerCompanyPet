using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using WorkerCompany.Authentication.Models;

namespace WorkerCompany.Authentication.AuthItems
{
    public class AuthSeeder
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<AppUser> userManager;

        public AuthSeeder()
        {

        }

        public AuthSeeder(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public AuthSeeder(IServiceProvider services)
        {
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                this.roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                this.userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            }
        }

        public async Task CreateRoles()
        {
            await CreateRole(Roles.Admin);
            await CreateRole(Roles.User);
        }

        public async Task CreateUsers()
        {
            await CreateUser("App admin", "admin@gmail.com", "Qwerty-1", Roles.Admin);
            await CreateUser("App test user", "testuser@gmail.com", "Qwerty-1", Roles.User);
        }

        private async Task CreateRole(string name)
        {
            var existingRole = roleManager.Roles.Any(r => r.Name.Equals(name));

            if (!existingRole)
            {
                var role = new IdentityRole
                {
                    Name = name,
                    NormalizedName = name.ToUpper()
                };
                var result = await roleManager.CreateAsync(role);
            }
        }

        private async Task CreateUser(string displayName, string email, string password, string role)
        {
            var existingUser = userManager.Users.Any(u => u.Email.Equals(email));

            if (!existingUser)
            {
                var user = new AppUser
                {
                    DisplayName = displayName,
                    Email = email,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, password);
                await AttachUserToRole(user, role);
            }
        }

        private async Task AttachUserToRole(AppUser user, string role)
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var existingRole = userRoles.Any(r => r.Equals(role));

            if (!existingRole)
            {
                var result = await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
