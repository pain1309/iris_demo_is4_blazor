using AutoMapper;
using Blazor.Wasm.gRPCClient.Models;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blazor.Wasm.gRPCClient.Services
{
    public class OrderingClientService : IOrderingClientService
    {
        private readonly HttpClient httpClient;

        public OrderingClientService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<Ordering> CreateOrdering(Ordering ordering)
        {
            using var response = await httpClient.PostAsJsonAsync("/Ordering", ordering);

            var data = await response.Content.ReadFromJsonAsync<Ordering>();

            return data;
        }

        public async Task<IEnumerable<Ordering>> GetOrderByUserName(string userName)
        {
            //    string path = $"/Ordering/GetOrderByUserName/{userName}";
            try
            {
                var getOrderingRequest = new OrderByUserNameRequest { UserName = userName };
                var channel = GrpcChannel.ForAddress("https://localhost:50051/routeguide.OrderingService/GetOrderByUserName");
                var client = new OrderingService.OrderingServiceClient(channel);
                var response = await client.GetOrderByUserNameAsync(getOrderingRequest);

                //var data = _mapper.Map<IEnumerable<Ordering>>(response.Ordering);

                return response.Ordering;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductByName(string productName)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<ProductViewModel>>("/Ordering/GetProduct/{productName}");
        }
    }
}
