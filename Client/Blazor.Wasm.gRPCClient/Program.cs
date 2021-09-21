using Blazor.Wasm.gRPCClient.Services;
using Grpc.Net.Client;
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

namespace Blazor.Wasm.gRPCClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSyncfusionBlazor();
            //Add gRPC service
            builder.Services.AddSingleton(services =>
            {
                // Get the service address from appsettings.json
                var config = services.GetRequiredService<IConfiguration>();
                var backendUrl = "https://localhost:50051/routeguide";

                // Create a channel with a GrpcWebHandler that is addressed to the backend server.
                //
                // GrpcWebText is used because server streaming requires it. If server streaming is not used in your app
                // then GrpcWeb is recommended because it produces smaller messages.
                // var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());

                return GrpcChannel.ForAddress(backendUrl);
            });

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTAwODIzQDMxMzkyZTMyMmUzMEJtL0NqNmdYRGVmU2pUMVVuaTRFektwWGV3VXFVbm0zNGl4WUF2SHprZnM9");
            builder.Services.AddScoped<IOrderingClientService, OrderingClientService>();

            await builder.Build().RunAsync();
        }
    }
}
