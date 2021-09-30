using Blazor.Wasm.BusinessClient.Refactor.Models;
using Blazor.Wasm.BusinessClient.Refactor.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Blazor.Wasm.BusinessClient.Services
{
    public class OrderingService : IOrderingService
    {
        private readonly HttpClient httpClient;

        public OrderingService(HttpClient httpClient)
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
            string path = $"/Ordering/GetOrderByUserName/{userName}";
            return (await httpClient.GetFromJsonAsync<OrderingResponse>(path)).Ordering;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductByName(string productName)
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<ProductViewModel>>("/Ordering/GetProduct/{productName}");
        }
    }
}
