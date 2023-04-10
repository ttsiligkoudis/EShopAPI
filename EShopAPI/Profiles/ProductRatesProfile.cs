using AutoMapper;
using DataModels;
using DataModels.Dtos;

namespace EShop.Profiles
{
    public class ProductRatesProfile : Profile
    {
        public ProductRatesProfile()
        {
            CreateMap<ProductRates, ProductRatesDto>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.CustomerName,
                        opt => opt.MapFrom(src => src.Customer.Name));
            CreateMap<ProductRatesDto, ProductRates>();
        }
    }
}
