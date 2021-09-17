using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Management.gRPC.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<Entities.Product, ProductModel>();
            CreateMap<Product, Entities.Product>().ReverseMap();
            CreateMap<ProductModel, Entities.Product>();
            CreateMap<ProductModel, Entities.Product>();
        }
    }
}
