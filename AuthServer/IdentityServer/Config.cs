using System.Collections.Generic;
using System.Security.Claims;
using AuthServer.Infrastructure.Data.Identity;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("businessServiceResource", "Business Service Resource")
                {
                    Scopes = { "businessService" },
                    UserClaims =
                    {
                        "role",
                        "given_name",
                        "family_name"
                    }
                },
                new ApiResource("configServiceResource", "Config Service Resource")
                {
                    Scopes = { "configService" },
                    UserClaims =
                    {
                        "role",
                        "given_name",
                        "family_name"
                    }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new ApiScope[]
            {
                new ApiScope("businessService", "Demo Business Service"),
                new ApiScope("configService", "Demo Config Serivce")
            };
        }

        public static List<TestUser> TestUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "2f47f8f0-bea1-4f0e-ade1-88533a0eaf57",
                    Username = "user1",
                    Password = "password1",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "firstName1"),
                        new Claim("family_name", "lastName1"),
                        new Claim("address", "USA"),
                        new Claim("email","user1@localhost"),
                        new Claim("phone", "123"),
                        new Claim("role", "Admin")
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client {
                    ClientId = "blazor",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "https://localhost:4201" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },
                    RedirectUris = { "https://localhost:4201/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:4201/authentication/logout-callback" }
                }
            };
        }
    }
}
