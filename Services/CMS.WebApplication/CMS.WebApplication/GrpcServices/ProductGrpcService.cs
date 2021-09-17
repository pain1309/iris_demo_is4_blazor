using AutoMapper;
using CMS.WebApplication.Models;
using Management.gRPC;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Management.gRPC.ProductService;

namespace CMS.WebApplication.GrpcServices
{
    public class ProductGrpcService
    {
        private readonly ProductServiceClient _productServiceClient;
        private readonly IMapper _mapper;
        public ProductGrpcService(ProductServiceClient productServiceClient, IMapper mapper)
        {
            _mapper = mapper;
            _productServiceClient = productServiceClient;
        }

        public async Task<List<ProductViewModel>> GetProducts()
        {
            var productRequest = new GetProductsRequest { };
            var products = await _productServiceClient.GetProductsAsync(productRequest);

            var productsResponse = new List<ProductModel>();
            productsResponse.AddRange(products.Product);

            var productViewModel = _mapper.Map<List<ProductViewModel>>(productsResponse);
            return productViewModel;
        }

        public async Task<ProductViewModel> GetProductById(int id)
        {
            var productRequest = new GetProductByIdRequest { Id = id };
            var product = await _productServiceClient.GetProductByIdAsync(productRequest);

            var productResponse = _mapper.Map<ProductViewModel>(product);
            return productResponse;
        }

        public ProductViewModel CreateProduct(ProductCreateViewModel model)
        {
            var productModelRequest = _mapper.Map<ProductModel>(model);
            var productCreateRequest = new CreateProductRequest();
            productCreateRequest.Product = productModelRequest;
            var product = _productServiceClient.CreateProduct(productCreateRequest);

            var productModel = _mapper.Map<ProductViewModel>(product);
            return productModel;
        }

        public ProductViewModel UpdateProduct(ProductViewModel model)
        {
            var productModelRequest = _mapper.Map<ProductModel>(model);
            var productUpdateRequest = new UpdateProductRequest();
            productUpdateRequest.Product = productModelRequest;
            var product = _productServiceClient.UpdateProduct(productUpdateRequest);

            var productModel = _mapper.Map<ProductViewModel>(product);
            return productModel;
        }

        public bool DeleteProduct(int id)
        {
            var productDeleteRequest = new DeleteProductRequest { Id = id };
            var response = _productServiceClient.DeleteProduct(productDeleteRequest);
            return response.Success;
        }
    }
}
