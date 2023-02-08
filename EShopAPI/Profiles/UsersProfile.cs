using AutoMapper;
using DataModels;
using DataModels.Dtos;

namespace EShop.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserDto, CustomerDto>()
                .ForMember(dest => dest.UserId,
                    opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id,
                    src => src.Ignore());
        }
    }
}
