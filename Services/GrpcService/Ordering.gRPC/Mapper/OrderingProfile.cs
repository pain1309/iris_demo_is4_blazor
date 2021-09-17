using AutoMapper;
using Domain.Entities;
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
            CreateMap<Ordering.gRPC.Entities.Product, ProductModel>();
            CreateMap<Product, Ordering.gRPC.Entities.Product>();
        }
    }
}
