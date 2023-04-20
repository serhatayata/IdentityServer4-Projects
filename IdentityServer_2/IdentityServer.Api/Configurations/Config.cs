using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer.Api.Configurations
{
    public static class Config
    {
        public static IEnumerable<ApiScope> ApiScopes =>
              new ApiScope[]
              {
                  
              };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
               
            };
    }
}
