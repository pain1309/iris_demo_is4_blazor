using Domain.Entities;
using Infrastructure.Interfaces;

namespace CMS.Web.Client.Repositories
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
    }
}
