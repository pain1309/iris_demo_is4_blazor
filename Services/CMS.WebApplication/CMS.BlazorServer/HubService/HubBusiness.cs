using AutoMapper;
using Domain.Entities;
using Domain.ViewModel;
using Management.gRPC;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Management.gRPC.ProductService;

namespace CMS.BlazorServer.HubService
{
    public class HubBusiness : Hub<IHubBusiness>
    {
        private readonly ProductServiceClient _productServiceClient;
        private readonly IMapper _mapper;
        public HubBusiness(ProductServiceClient productServiceClient, IMapper mapper)
        {
            _mapper = mapper;
            _productServiceClient = productServiceClient;
        }
        public async Task CreateProduct(Product product)
        {
            var productModelRequest = _mapper.Map<ProductModel>(product);
            var productCreateRequest = new CreateProductRequest();
            productCreateRequest.Product = productModelRequest;
            _productServiceClient.CreateProduct(productCreateRequest);
            await GetProducts();
        }

        //public async Task UpdateProduct(Product product)
        //{
        //    var productModelRequest = _mapper.Map<ProductModel>(product);
        //    var productUpdateRequest = new UpdateProductRequest();
        //    productUpdateRequest.Product = productModelRequest;
        //    _productServiceClient.UpdateProduct(productUpdateRequest);

        //    await Clients.All.SendAsync("ExchangeDataProduct", product);
        //}

        public async Task DeleteProduct(int id)
        {
            var productDeleteRequest = new DeleteProductRequest { Id = id };
            var response = _productServiceClient.DeleteProduct(productDeleteRequest);

            await GetProducts();
        }

        public async Task GetProducts()
        {
            var productRequest = new GetProductsRequest { };
            var products = await _productServiceClient.GetProductsAsync(productRequest);

            var productsResponse = new List<ProductModel>();
            productsResponse.AddRange(products.Product);

            var productViewModel = _mapper.Map<List<ProductViewModel>>(productsResponse);

            await Clients.All.GetProducts(productViewModel);
        }

        //public async Task GetProductById(int id)
        //{
        //    var productRequest = new GetProductByIdRequest { Id = id };
        //    var product = await _productServiceClient.GetProductByIdAsync(productRequest);

        //    var productResponse = _mapper.Map<ProductViewModel>(product);

        //    await Clients.All.SendAsync(productResponse);
        //}
    }

    public interface IHubBusiness
    {
        Task GetProducts(List<ProductViewModel> products);
        Task CreateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
