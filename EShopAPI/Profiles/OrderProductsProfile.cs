using AutoMapper;
using DataModels;
using DataModels.Dtos;

namespace EShop.Profiles
{
    public class OrderProductsProfile : Profile
    {
        public OrderProductsProfile()
        {
            CreateMap<OrderProducts, OrderProductsDto>();
                //.ForMember(dest => dest.ProductName,
                //    opt => opt.MapFrom(src => src.Product.Name))
                //.ForMember(dest => dest.ProductCategory,
                //    opt => opt.MapFrom(src => src.Product.Category));
                CreateMap<OrderProductsDto, OrderProducts>();
                //.ForPath(dest => dest.Product.Name,
                //    opt => opt.MapFrom(src => src.ProductName))
                //.ForPath(dest => dest.Product.Category,
                //    opt => opt.MapFrom(src => src.ProductCategory))
                //.ForPath(dest => dest.Product.Id,
                //    opt => opt.MapFrom(src => src.ProductId));
        }
    }
}
