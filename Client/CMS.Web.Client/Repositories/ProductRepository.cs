using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories;

namespace CMS.Web.Client.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        public ProductRepository(OrderContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }
    }
}
