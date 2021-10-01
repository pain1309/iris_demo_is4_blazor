using AuthServer.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blazor.Wasm.BusinessClient.IndividualUserAccounts2.Services
{
    //public class AuthStateProvider : AuthenticationStateProvider
    //{

    //    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    //    {
    //        //First get user claims    
    //        var anonymous = new ClaimsIdentity();
    //        return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
    //    }
    //}
}
