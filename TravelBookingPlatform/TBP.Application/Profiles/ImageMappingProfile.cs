using AutoMapper;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.MappingProfiles;

public class ImageMappingProfile : Profile
{
    public ImageMappingProfile()
    {
        ImageMapping();
    }

    private void ImageMapping()
    {
        CreateMap<CreateImageDto, Image>();
        CreateMap<Image, CreateImageDto>();

        CreateMap<UpdateImageDto, Image>();
        CreateMap<Image, UpdateImageDto>();

        CreateMap<ImageDto, Image>();
        CreateMap<Image, ImageDto>();
    }
}