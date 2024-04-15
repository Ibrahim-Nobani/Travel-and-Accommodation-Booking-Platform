using AutoMapper;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.MappingProfiles;

public class FeaturedDealMappingProfile : Profile
{
    public FeaturedDealMappingProfile()
    {
        FeaturedDealMapping();
    }

    private void FeaturedDealMapping()
    {
        CreateMap<CreateFeaturedDealDto, FeaturedDeal>();
        CreateMap<FeaturedDeal, CreateFeaturedDealDto>();

        CreateMap<UpdateFeaturedDealDto, FeaturedDeal>();
        CreateMap<FeaturedDeal, UpdateFeaturedDealDto>();

        CreateMap<FeaturedDealDto, FeaturedDeal>();
        CreateMap<FeaturedDeal, FeaturedDealDto>();
    }
}