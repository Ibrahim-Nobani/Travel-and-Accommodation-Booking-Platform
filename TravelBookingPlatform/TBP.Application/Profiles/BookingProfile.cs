using AutoMapper;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.MappingProfiles;

public class BookingMappingProfile : Profile
{
    public BookingMappingProfile()
    {
        BookingMapping();
    }

    private void BookingMapping()
    {
        CreateMap<CreateBookingDto, Booking>();
        CreateMap<Booking, CreateBookingDto>();

        CreateMap<BookingDto, Booking>();
        CreateMap<Booking, BookingDto>();
    }
}