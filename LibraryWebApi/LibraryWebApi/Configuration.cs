using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4;

namespace LibraryWebApi
{
    public static class Configuration
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            yield return new ApiResource("LibraryWebApi");
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Profile();
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            yield return new ApiScope("LibraryWebApi", "Web API");
        }

        public static IEnumerable<Client> GetClients() =>
       new List<Client>
       {
              new Client
              {
                  ClientId = "client_id_cf",
                  ClientSecrets = { new Secret("client_secret_cf".ToSha256()) },

                  AllowedGrantTypes =  GrantTypes.ResourceOwnerPassword,
             
                  AllowedScopes =
                  {
                      "LibraryWebApi",
                      IdentityServerConstants.StandardScopes.OpenId,
                      IdentityServerConstants.StandardScopes.Profile
                  },

                  RedirectUris = { "https://localhost:7076/signin-oidc" },

                  RequireConsent = false           
              }
       };
    }
}