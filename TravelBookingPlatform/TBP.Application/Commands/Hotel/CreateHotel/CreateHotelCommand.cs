using MediatR;
using TravelBookingPlatform.Application.DTOs;

namespace TravelBookingPlatform.Application.Commands;

public class CreateHotelCommand : IRequest<HotelDto>
{
    public CreateHotelDto CreateHotelDto { get; set; }
    public int CityId { get; set; }
}