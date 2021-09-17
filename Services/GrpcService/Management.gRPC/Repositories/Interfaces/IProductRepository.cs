using Domain.Entities;
using Infrastructure.Interfaces;
using Management.gRPC.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Management.gRPC.Repositories.Interfaces
{
    public interface IProductRepository : IAsyncRepository<Domain.Entities.Product>
    {
        Task<List<Management.gRPC.Entities.Product>> GetProducts();
        Task<List<Management.gRPC.Entities.Product>> GetProductsByName(string productName);
        Task<bool> CreateProduct(Management.gRPC.Entities.Product product);
        Task<bool> UpdateProduct(Management.gRPC.Entities.Product product);
        Task<bool> DeleteProduct(int id);
        Task<Entities.Product> GetProductByIdAsync(int id);
    }
}
