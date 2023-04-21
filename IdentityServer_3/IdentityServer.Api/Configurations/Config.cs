using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer.Api.Configurations
{
    public static class Config
    {
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "jsClient",
                    ClientName = "Javascript Client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5003/callback.html" },
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5003/index.html"  },
                    AllowedCorsOrigins =     { "https://localhost:5003" },
                    AllowOfflineAccess = true,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
                new List<IdentityResource>
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile()
        };
    }
}
