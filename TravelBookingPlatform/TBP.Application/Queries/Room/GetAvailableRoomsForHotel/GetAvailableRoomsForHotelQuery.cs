using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetAvailableRoomsForHotelQuery : IRequest<IEnumerable<RoomDto>>
{
    public int HotelId { get; set; }
}