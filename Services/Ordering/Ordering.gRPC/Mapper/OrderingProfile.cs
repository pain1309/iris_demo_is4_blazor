using AutoMapper;
using Domain.Entities;
using Ordering.gRPC;
using Ordering.gRPCClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.gRPC.Mapper
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<Order, Ordering.gRPCClient.Ordering>().ReverseMap();
            CreateMap<OrderingRequest, Order>().ReverseMap();
            CreateMap<OrderingRequest, OrderingReply>().ReverseMap();
        }
    }
}
