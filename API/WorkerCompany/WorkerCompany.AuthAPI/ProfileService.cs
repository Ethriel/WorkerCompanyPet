using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.AuthAPI
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory;

        public ProfileService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var id = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userClaims = await userClaimsPrincipalFactory.CreateAsync(user);
                var claims = userClaims.Claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
                if (userManager.SupportsUserRole)
                {
                    var roleNames = await userManager.GetRolesAsync(user);
                    var role = default(IdentityRole);
                    var roleClaims = default(IList<Claim>);
                    foreach (var roleName in roleNames)
                    {
                        claims.Add(new Claim(JwtClaimTypes.Role, roleName));
                        if (roleManager.SupportsRoleClaims)
                        {
                            role = await roleManager.FindByNameAsync(roleName);
                            if (role != null)
                            {
                                roleClaims = await roleManager.GetClaimsAsync(role);
                                claims.AddRange(roleClaims);
                            }
                        }
                    }
                }

                context.IssuedClaims = claims;
                //var userClaims = roles.Select(role => new Claim(JwtClaimTypes.Role, role));
                //context.IssuedClaims.AddRange(userClaims);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var id = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(id);
            context.IsActive = user != null;
        }
    }
}
