using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerCompany.AuthAPI.Helpers.Create;

namespace WorkerCompany.AuthAPI.Helpers
{
    public class IdentityServerConfig
    {
        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>
        {
            new ApiResource("api", "Project API")
        };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope>
        {
            CreateIdentityItems.CreateApiScope("api", "Project API")
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            CreateIdentityItems.CreateDefaultClient(),
            CreateIdentityItems.CreatePostManClient(),
            CreateIdentityItems.CreateClient("main-api", "Main API", "https://localhost:5001")
        };

        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            CreateIdentityItems.CreateIdentityResource("role", "Role", JwtClaimTypes.Role)
        };
    }
}
