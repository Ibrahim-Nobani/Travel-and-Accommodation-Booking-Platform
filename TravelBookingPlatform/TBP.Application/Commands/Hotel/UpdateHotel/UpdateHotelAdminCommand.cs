using MediatR;
using TravelBookingPlatform.Application.DTOs;

namespace TravelBookingPlatform.Application.Commands;

public class UpdateHotelAdminCommand : IRequest<HotelDto>
{
    public int HotelId { get; set; }
    public UpdateHotelDto UpdateHotelDto { get; set; }
}