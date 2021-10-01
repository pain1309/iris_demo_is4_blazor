using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazor.Wasm.BusinessClient.IndividualUserAccounts3.Services;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Blazor.Wasm.BusinessClient.IndividualUserAccounts3.Handler;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Blazor.Wasm.BusinessClient.IndividualUserAccounts3
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSyncfusionBlazor();
            builder.Services.AddAuthorizationCore();

            //builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));
            builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

            builder.Services.AddHttpClient("ServerAPI", client => client.BaseAddress = new Uri("http://localhost:5010"))
               .AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

            builder.Services.AddTransient(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("ServerAPI"));

            //builder.Services.AddHttpClient<IOrderingService, OrderingService>(client =>
            //{
            //    client.BaseAddress = new Uri("http://localhost:5010/");
            //});
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTAwODIzQDMxMzkyZTMyMmUzMEJtL0NqNmdYRGVmU2pUMVVuaTRFektwWGV3VXFVbm0zNGl4WUF2SHprZnM9");
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5010/") });
            builder.Services.AddScoped<IOrderingService, OrderingService>();
            //builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                // Configure you’re authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                //options.ProviderOptions.Authority = "https://localhost:5000"; //The IdentityServer URL 
                //options.ProviderOptions.ClientId = "blazor"; // The client ID
                //options.ProviderOptions.ResponseType = "code";
                builder.Configuration.Bind("oidc", options.ProviderOptions);
            });

            await builder.Build().RunAsync();
        }
    }
}
