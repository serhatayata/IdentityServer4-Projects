using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer.Api.Configurations
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
              new ApiScope[]
              {
                  new ApiScope("catalog_readpermission","Full access to Catalog API"),
                  new ApiScope("catalog_writepermission","Full access to Catalog API"),
                  new ApiScope("order_readpermission","Full Access to Order API"),
                  new ApiScope("order_writepermission","Full Access to Order API")
              };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               new Client
               {
                  ClientName="ClientFirst",
                  ClientId="clientFirst",
                  ClientSecrets= {new Secret("clientfirstsecret".Sha256())},
                  AllowedGrantTypes= GrantTypes.ClientCredentials,
                  AllowedScopes={ "catalog_readpermission", "catalog_writepermission" }
               },
               new Client
               {
                  ClientName="ClientSecond",
                  ClientId="clientSecond",
                  ClientSecrets= {new Secret("clientsecondsecret".Sha256())},
                  AllowedGrantTypes= GrantTypes.ClientCredentials,
                  AllowedScopes={ "order_readpermission", "order_writepermission" }
               }
            };
    }
}
