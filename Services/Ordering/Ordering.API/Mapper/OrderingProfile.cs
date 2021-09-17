using AutoMapper;
using Domain.Entities;
using Ordering.API.Entities;
using Ordering.gRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Mapper
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<Ordering.API.Entities.Ordering, Order>().ReverseMap();
            CreateMap<ProductModel, ProductViewModel>().ReverseMap();
        }
    }
}
