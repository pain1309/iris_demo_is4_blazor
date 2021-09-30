using Blazor.Wasm.BusinessClient.Refactor.Security;
using Blazor.Wasm.BusinessClient.Refactor.Services;
using Blazor.Wasm.BusinessClient.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
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

namespace Blazor.Wasm.BusinessClient.Refactor
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

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<CustomStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("oidc", options.ProviderOptions);
            }).AddAccountClaimsPrincipalFactory<ArrayClaimsPrincipalFactory<RemoteUserAccount>>();

            await builder.Build().RunAsync();
        }
    }
}
