using Blazor.Wasm.BusinessClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Wasm.BusinessClient.Services
{
    public interface IOrderingService
    {
        Task<IEnumerable<ProductViewModel>> GetProductByName(string productName);
        Task<IEnumerable<Ordering>> GetOrderByUserName(string userName);
        Task<Ordering> CreateOrdering(Ordering ordering);
    }
}
