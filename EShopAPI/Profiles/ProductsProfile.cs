using AutoMapper;
using DataModels;
using DataModels.Dtos;

namespace EShop.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Rate,
                    opt => opt.MapFrom(src => src.ProductRates.Select(s => s.Rate).ToList().DefaultIfEmpty(0).Average()));
            CreateMap<ProductDto, Product>();

        }
    }
}
