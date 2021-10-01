// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer2
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(), //standard openid (subject id)
                new IdentityResources.Profile(), //(first name, last name etc..) 
            };

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("businessServiceResource", "Business Service Resource")
                {
                    Scopes = { "businessServiceResource" },
                    UserClaims =
                    {
                        "role",
                        "given_name",
                        "family_name"
                    }
                },
                new ApiResource("configServiceResource", "Config Service Resource")
                {
                    Scopes = { "configServiceResource" },
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
            return new List<ApiScope>
            {
                new ApiScope("businessServiceResource", "Demo Business Service"),
                new ApiScope("configServiceResource", "Demo Config Serivce")
            };
        }

        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                new Client
                {
                    ClientId = "blazor3",
                    ClientName = "Blazor WebAssembly App Client",
                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    AllowedCorsOrigins = { "https://localhost:4203" },
                    RedirectUris = { "https://localhost:4203/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:4203/authentication/logout-callback" },

                    AllowedScopes = {"openid", "profile", "businessServiceResource"},
                },
                new Client
                {
                    ClientId = "blazor2",
                    ClientName = "Blazor WebAssembly App Client",
                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    AllowedCorsOrigins = { "https://localhost:4202" },
                    RedirectUris = { "https://localhost:4202/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:4202/authentication/logout-callback" },

                    AllowedScopes = {"openid", "profile", "configServiceResource"},
                }
            };
    }
}