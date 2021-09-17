using Ordering.gRPC;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Ordering.gRPC.OrderingService;

namespace Ordering.API.GrpcServices
{
    public class OrderingGrpcService
    {
        private readonly OrderingServiceClient _orderingServiceClient;
        public OrderingGrpcService(OrderingServiceClient orderingServiceClient)
        {
            _orderingServiceClient = orderingServiceClient;
        }

        public async Task<List<ProductModel>> GetProductsByName(string productName)
        {
            var createOrderingRequest = new GetProductRequest { ProductName = productName };
            var replyModel = await _orderingServiceClient.GetProductsAsync(createOrderingRequest);
            var listProductReturn = new List<ProductModel>();
            listProductReturn.AddRange(replyModel.Product);
            return listProductReturn;
        }
    }
}
