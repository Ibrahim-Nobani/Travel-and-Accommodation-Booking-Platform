using AutoMapper;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.MappingProfiles;

public class RecentlyVisitedHotelMappingProfile : Profile
{
    public RecentlyVisitedHotelMappingProfile()
    {
        RecentlyVisitedHotelMapping();
    }

    private void RecentlyVisitedHotelMapping()
    {
        CreateMap<RecentlyVisitedHotelView, RecentlyVisitedHotelDto>();
        CreateMap<RecentlyVisitedHotelDto, RecentlyVisitedHotelView>();
    }
}