using AutoMapper;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.MappingProfiles;

public class CityMappingProfile : Profile
{
    public CityMappingProfile()
    {
        CityMapping();
    }

    private void CityMapping()
    {
        CreateMap<CreateCityDto, City>();
        CreateMap<City, CreateCityDto>();

        CreateMap<UpdateCityDto, City>();
        CreateMap<City, UpdateCityDto>();

        CreateMap<CityDto, City>();
        CreateMap<City, CityDto>();
    }
}