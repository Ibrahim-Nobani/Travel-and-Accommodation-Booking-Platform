using AutoMapper;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.MappingProfiles;

public class HotelMappingProfile : Profile
{
    public HotelMappingProfile()
    {
        HotelMapping();
    }

    private void HotelMapping()
    {
        CreateMap<CreateHotelDto, Hotel>();
        CreateMap<Hotel, CreateHotelDto>();

        CreateMap<UpdateHotelDto, Hotel>();
        CreateMap<Hotel, UpdateHotelDto>();

        CreateMap<HotelDto, Hotel>();
        CreateMap<Hotel, HotelDto>();
    }
}