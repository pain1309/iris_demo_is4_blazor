using Blazor.Wasm.BusinessClient.IndividualUserAccounts.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Wasm.BusinessClient.IndividualUserAccounts
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddSyncfusionBlazor();

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTAwODIzQDMxMzkyZTMyMmUzMEJtL0NqNmdYRGVmU2pUMVVuaTRFektwWGV3VXFVbm0zNGl4WUF2SHprZnM9");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5010/") });
            builder.Services.AddScoped<IOrderingService, OrderingService>();

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("oidc", options.ProviderOptions);
            });

            await builder.Build().RunAsync();
        }
    }
}
