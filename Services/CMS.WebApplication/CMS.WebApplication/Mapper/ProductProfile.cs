using AutoMapper;
using CMS.WebApplication.Models;
using Management.gRPC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.WebApplication.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, UpdateProductRequest>();
            CreateMap<ProductCreateViewModel, ProductModel>();
            CreateMap<ProductModel, ProductViewModel>().ReverseMap();
            CreateMap<ProductCreateViewModel, CreateProductRequest>();
        }
    }
}
