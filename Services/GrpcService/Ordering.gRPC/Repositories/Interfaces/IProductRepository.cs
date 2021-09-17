using Domain.Entities;
using Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.gRPC.Repositories.Interfaces
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
        Task<List<Ordering.gRPC.Entities.Product>> GetProductsByName(string productName);
    }
}
