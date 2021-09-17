using Microsoft.Extensions.Logging;
using AutoMapper;
using static Ordering.gRPC.OrderingService;
using System.Threading.Tasks;
using Ordering.gRPC.Repositories.Interfaces;
using System.Collections.Generic;
using Grpc.Core;

namespace Ordering.gRPC.Services
{
    public class OrderingService : OrderingServiceBase
    {
        private readonly ILogger<OrderingService> _logger;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public OrderingService(ILogger<OrderingService> logger, IMapper mapper, IProductRepository productRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public override async Task<ReplyModel> GetProducts(Ordering.gRPC.GetProductRequest request, ServerCallContext context)
        {
            var products = await _productRepository.GetProductsByName(request.ProductName);

            var productsConvertModel = _mapper.Map<IEnumerable<ProductModel>>(products);
            var replyModel = new ReplyModel();
            replyModel.Product.AddRange(productsConvertModel);
            return replyModel;
        }
    }
}
