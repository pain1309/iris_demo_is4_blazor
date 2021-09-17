using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.WebApplication.Repositories
{
    public interface IProductRepository : IAsyncRepository<Product>
    {
    }
}
