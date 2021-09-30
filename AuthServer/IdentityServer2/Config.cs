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

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                new Client
                {
                    ClientId = "blazor",
                    ClientName = "Blazor WebAssembly App Client",
                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    AllowedCorsOrigins = { "https://localhost:4201" },
                    RedirectUris = { "https://localhost:4201/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:4201/authentication/logout-callback" },

                    AllowedScopes = {"openid", "profile"},
                }
            };
    }
}