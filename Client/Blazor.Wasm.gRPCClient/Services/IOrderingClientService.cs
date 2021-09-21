using Blazor.Wasm.gRPCClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Wasm.gRPCClient.Services
{
    public interface IOrderingClientService
    {
        Task<IEnumerable<ProductViewModel>> GetProductByName(string productName);
        Task<IEnumerable<Ordering>> GetOrderByUserName(string userName);
        Task<Ordering> CreateOrdering(Ordering ordering);
    }
}
