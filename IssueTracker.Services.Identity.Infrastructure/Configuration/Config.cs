using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Identity.Infrastructure.Configuration
{
	public class Config
	{
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("IdentityService", "Identity Service"),
                 new ApiResource("IssuesService.API", "IssuesService.API")
                {
                    Scopes = 
                    {
                        "IssuesService.API"
                    },
                    ApiSecrets = { new Secret("secret".Sha256()) }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                // backward compat
                new ApiScope("IssuesService.API"),
                new ApiScope("IdentityService")
            };
        }
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = configuration.GetValue<string>("AuthSettings:Swagger:ClientId"),
                    ClientName = "MVC Client",
                    ClientSecrets = new List<Secret>
                    {
                        new Secret(configuration.GetValue<string>("AuthSettings:Swagger:Secret").Sha256())
                    },
                    ClientUri = configuration.GetValue<string>("AuthSettings:Swagger:RedirectURL"),
                    AllowedGrantTypes = GrantTypes.Code,
                    AccessTokenType=AccessTokenType.Reference,
                    AllowAccessTokensViaBrowser = true,
                     AlwaysSendClientClaims = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    RequirePkce=true,
                    RedirectUris = new List<string>
                    {
                       configuration.GetValue<string>("AuthSettings:Swagger:RedirectURL"),
                      // "https://localhost:44397/signin-oidc"
                    },
                    //PostLogoutRedirectUris = new List<string>
                    //{
                    //   configuration.GetValue<string>("AuthSettings:Swagger:PostLogoutRedirectURL")
                    //},
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "IssuesService.API"

                    },
                    AccessTokenLifetime = 60*60*2, // 2 hours
                    IdentityTokenLifetime= 60*60*2 // 2 hours
                },
            };
        }

    }
}
