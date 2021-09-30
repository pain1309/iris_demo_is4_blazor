using Blazor.Wasm.BusinessClient.Security;
using Blazor.Wasm.BusinessClient.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazor.Wasm.BusinessClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSyncfusionBlazor();
            //builder.Services.AddHttpClient<IOrderingService, OrderingService>(client =>
            //{
            //    client.BaseAddress = new Uri("http://localhost:5010/");
            //});
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTAwODIzQDMxMzkyZTMyMmUzMEJtL0NqNmdYRGVmU2pUMVVuaTRFektwWGV3VXFVbm0zNGl4WUF2SHprZnM9");
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5010/") });
            builder.Services.AddScoped<IOrderingService, OrderingService>();

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("Local", options.ProviderOptions);
            })

                .AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

            await builder.Build().RunAsync();
        }
    }
}
