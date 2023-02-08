using AutoMapper;
using DataModels;
using DataModels.Dtos;

namespace EShop.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

        }
    }
}
