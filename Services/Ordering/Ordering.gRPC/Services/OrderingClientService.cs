using AutoMapper;
using Domain.Entities;
using Grpc.Core;
using Grpc.Net.Client;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Ordering.gRPCClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Ordering.gRPCClient.OrderingService;

namespace Ordering.gRPC
{
    public class OrderingClientService : OrderingService.OrderingServiceBase
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger<OrderingClientService> _logger;
        private readonly IMapper _mapper;
        public OrderingClientService(ILogger<OrderingClientService> logger, IOrderRepository repository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task<ReplyModel> GetProducts(GetProductRequest request, ServerCallContext context)
        {
            var createOrderingRequest = new GetProductRequest { ProductName = request.ProductName };
            //var replyModel = await _orderingServiceClient.GetProductsAsync(createOrderingRequest);
            var channel = GrpcChannel.ForAddress("https://localhost:5005");
            var client = new OrderingServiceClient(channel);
            var response = await client.GetProductsAsync(createOrderingRequest);
            
            return response;
        }


        public override async Task<OrderingReply> GetOrderByUserName(OrderByUserNameRequest request, ServerCallContext context)
        {
            var orders = await _repository.GetOrdersByUserName(request.UserName);
            var listOrderingReturn = new OrderingReply();
            var orderConvert = _mapper.Map<List<Ordering.gRPCClient.Ordering>>(orders);
            listOrderingReturn.Ordering.AddRange(orderConvert);
            return listOrderingReturn;
        }

        public override async Task<OrderingReply> CreateOrder(OrderingRequest ordering, ServerCallContext context)
        {
            var orderEntity = _mapper.Map<Order>(ordering);
            var orderCreateResult = await _repository.CreateOrder(orderEntity);
            var orderReturn = _mapper.Map<OrderingReply>(ordering);
            return orderReturn;
        }

    }
}
