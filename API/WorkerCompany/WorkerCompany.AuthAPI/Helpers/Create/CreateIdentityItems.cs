using IdentityServer4;
using IdentityServer4.Models;

namespace WorkerCompany.AuthAPI.Helpers.Create
{
    public static class CreateIdentityItems
    {
        public const string ApiScope = "api1";
        public const string RolesScope = "roles";
        public static string SecretSha256 { get => "B5DD15DC-0B6F-4648-99B5-EC43CCD34923".Sha256(); }

        public static ApiScope CreateApiScope(string name, string displayName)
        {
            return new ApiScope(name, displayName);
        }

        public static Client CreateClient(string clientId, string clientName, string clientUri)
        {
            return new Client
            {
                ClientId = clientId,
                ClientName = clientName,
                RedirectUris = { $"{clientUri}/signin-oidc" },
                PostLogoutRedirectUris = { $"{clientUri}/signout-callback-oidc" },
                ClientSecrets = { new Secret(SecretSha256) },
                AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                RequireConsent = false,
                AllowOfflineAccess = true,
                RequirePkce = false,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    ApiScope,
                    RolesScope
                }
            };
        }

        public static Client CreateDefaultClient()
        {
            return new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret(SecretSha256) },
                AllowedScopes = { ApiScope }
            };
        }

        public static Client CreatePostManClient()
        {
            return new Client
            {
                ClientId = "postman",
                RequirePkce = true,
                Enabled = true,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret(SecretSha256)},
                AllowedScopes =
                {
                    ApiScope,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            };
        }

        public static IdentityResource CreateIdentityResource(string name, string displayName, string claimType)
        {
            return new IdentityResource
            {
                Name = name,
                DisplayName = displayName,
                UserClaims = { claimType }
            };
        }
    }
}
