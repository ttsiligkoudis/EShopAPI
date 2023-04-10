using AutoMapper;
using DataModels;
using DataModels.Dtos;

namespace EShop.Profiles
{
    public class OrderProductsProfile : Profile
    {
        public OrderProductsProfile()
        {
            CreateMap<OrderProducts, OrderProductsDto>()
                .ForMember(dest => dest.ProductName,
                    opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<OrderProductsDto, OrderProducts>();
        }
    }
}
