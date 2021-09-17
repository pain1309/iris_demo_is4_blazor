

using AutoMapper;
using CMS.BlazorServer.Models;
using Domain.Entities;
using Management.gRPC;

namespace CMS.BlazorServer.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductViewModel, UpdateProductRequest>();
            CreateMap<ProductCreateViewModel, ProductModel>();
            CreateMap<ProductModel, Domain.ViewModel.ProductViewModel>().ReverseMap();
            CreateMap<ProductModel, ProductViewModel>().ReverseMap();
            CreateMap<ProductCreateViewModel, CreateProductRequest>();
            CreateMap<Product, ProductModel>().ReverseMap();
        }
    }
}
