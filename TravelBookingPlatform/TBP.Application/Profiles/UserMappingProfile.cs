using AutoMapper;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        UserMapping();
    }

    private void UserMapping()
    {
        CreateMap<LoginUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        CreateMap<User, LoginUserDto>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));

        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        CreateMap<User, CreateUserDto>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));

        CreateMap<CreateUserAdminDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        CreateMap<User, CreateUserAdminDto>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));

        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        CreateMap<User, UpdateUserDto>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));

        CreateMap<UpdateUserAdminDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        CreateMap<User, UpdateUserAdminDto>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));

        CreateMap<UserResponseDto, User>();
        CreateMap<User, UserResponseDto>();

        CreateMap<UserAdminResponseDto, User>();
        CreateMap<User, UserAdminResponseDto>();
    }
}