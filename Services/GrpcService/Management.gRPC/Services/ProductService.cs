using Microsoft.Extensions.Logging;
using static Management.gRPC.ProductService;
using System.Threading.Tasks;
using Management.gRPC.Repositories.Interfaces;
using System.Collections.Generic;
using Grpc.Core;
using AutoMapper;

namespace Management.gRPC.Services
{
    public class ProductManagementService : ProductServiceBase
    {
        private readonly ILogger<ProductManagementService> _logger;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductManagementService(ILogger<ProductManagementService> logger, IMapper mapper, IProductRepository productRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public override async Task<ReplyModel> GetProducts(Management.gRPC.GetProductsRequest request, ServerCallContext context)
        {
            try
            {
                var products = await _productRepository.GetProducts();

                var productsConvertModel = _mapper.Map<List<ProductModel>>(products);
                var replyModel = new ReplyModel();
                replyModel.Product.AddRange(productsConvertModel);
                return replyModel;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
        public override async Task<ProductModel> GetProductById(Management.gRPC.GetProductByIdRequest request, ServerCallContext context)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);

            var productConvertModel = _mapper.Map<ProductModel>(product);
            return productConvertModel;
        }

        public override async Task<ReplyModel> GetProductsByName(Management.gRPC.GetProductsByNameRequest request, ServerCallContext context)
        {
            var products = await _productRepository.GetProductsByName(request.ProductName);

            var productsConvertModel = _mapper.Map<IEnumerable<ProductModel>>(products);
            var replyModel = new ReplyModel();
            replyModel.Product.AddRange(productsConvertModel);
            return replyModel;
        }

        public override async Task<ProductModel> CreateProduct(Management.gRPC.CreateProductRequest request, ServerCallContext context)
        {
            var productCreate = _mapper.Map<Entities.Product>(request.Product);
            await _productRepository.CreateProduct(productCreate);

            var productModel = _mapper.Map<ProductModel>(productCreate);
            return productModel;
        }
        public override async Task<ProductModel> UpdateProduct(Management.gRPC.UpdateProductRequest request, ServerCallContext context)
        {
            var productUpdate = _mapper.Map<Entities.Product>(request.Product);
            await _productRepository.UpdateProduct(productUpdate);

            var productModel = _mapper.Map<ProductModel>(productUpdate);
            return productModel;
        }
        public override async Task<DeleteProductResponse> DeleteProduct(Management.gRPC.DeleteProductRequest request, ServerCallContext context)
        {
            var deleted = await _productRepository.DeleteProduct(request.Id);

            var response = new DeleteProductResponse
            {
                Success = deleted
            };

            return response;
        }
    }
}
