using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Ordering.gRPC.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.gRPC.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        public ProductRepository(OrderContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }
        public async Task<List<Ordering.gRPC.Entities.Product>> GetProductsByName(string productName)
        {
            var products = await _dbContext.Products.Where(p => p.Name == productName).ToListAsync();
            var productsResult = _mapper.Map<List<Ordering.gRPC.Entities.Product>>(products);
            return productsResult;
        }
    }
}
