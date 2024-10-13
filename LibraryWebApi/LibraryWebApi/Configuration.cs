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
            yield return new ApiResource("Infrastructure");
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Profile();
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            yield return new ApiScope("LibraryWebApi", "Web API");
            yield return new ApiScope("Infrastructure", "Web API");
        }

        public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
              new Client
              {
                  ClientId = "client_id",
                  ClientSecrets = { new Secret("client_secret".ToSha256()) },
                  AllowedGrantTypes =  GrantTypes.Code,
                  AllowedCorsOrigins = { "https://localhost:5233" },
                  RefreshTokenExpiration = TokenExpiration.Absolute,
                  AbsoluteRefreshTokenLifetime = 2592000,
                  AllowedScopes =
                  {
                      "LibraryWebApi",
                      "Infrastructure",
                      IdentityServerConstants.StandardScopes.OpenId,
                      IdentityServerConstants.StandardScopes.Profile
                  },
                  RequireConsent = false
              },
        };

        /*    public static IEnumerable<ApiScope> ApiScopes =>
               new List<ApiScope>
               {
                    new ApiScope("LibraryWebApi", "Library Web API")
               };
            public static IEnumerable<IdentityResource> IdentityResources =>
                new List<IdentityResource>
                {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile()
                };
            public static IEnumerable<ApiResource> ApiResources =>
                new List<ApiResource>
                {
                    new ApiResource("LibraryWebApi", "Library Web API", new []
                        { JwtClaimTypes.Name})
                    {
                        Scopes = { "LibraryWebApi" }
                    }
                };
            public static IEnumerable<Client> Clients =>
                new List<Client>
                {
                    new Client
                    {
                        ClientId = "library-web-api",
                        ClientName = "Library Web",
                        AllowedGrantTypes = GrantTypes.Code,
                        RequireClientSecret = false,
                        RequirePkce = true,
                        RedirectUris =
                        {
                            "http://.../signin-oidc"
                        },
                        AllowedCorsOrigins =
                        {
                            "http://..."
                        },
                        PostLogoutRedirectUris =
                        {
                            "http:/.../signout-oidc"
                        },
                        AllowedScopes =
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            "LibraryWebApi"
                        },
                        AllowAccessTokensViaBrowser = true
                    }
                };*/
        /* public static IEnumerable<ApiScope> ApiScopes =>
             new List<ApiScope>
             {
                 new ApiScope("LibraryWebApi", "Web API")
             };

         public static IEnumerable<IdentityResource> IdentityResources =>
             new List<IdentityResource>
             {
                 new IdentityResources.OpenId(),
                 new IdentityResources.Profile()
             };


         public static IEnumerable<ApiResource> ApiResources =>
             new List<ApiResource>
             {
                 new ApiResource("LibraryWebApi", "Web API", new []
                     { JwtClaimTypes.Name})
                 {
                     Scopes = { "LibraryWebApi" }
                 }
             };*/
        /*public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                  new Client
                  {
                      ClientId = "client_id_swagger",
                      ClientSecrets = { new Secret("client_secret_swagger".ToSha256()) },
                      AllowedGrantTypes =  GrantTypes.ClientCredentials,
                      AllowedCorsOrigins = { "https://localhost:5233" },
                      AllowedScopes =
                      {
                          "SwaggerAPI",
                          IdentityServerConstants.StandardScopes.OpenId,
                          IdentityServerConstants.StandardScopes.Profile
                      }
                      },
               /* new Client
                {
                    ClientId = "library-web-api",
                    ClientName = "Library Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        "http://.../signin-oidc"
                    },
                    AllowedCorsOrigins =
                    {
                        "http://..."
                    },
                    PostLogoutRedirectUris =
                    {
                        "http:/.../signout-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "LibraryWebApi"
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };*/
    }
}