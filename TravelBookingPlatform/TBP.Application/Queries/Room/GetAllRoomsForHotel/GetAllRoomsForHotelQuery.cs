using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetAllRoomsForHotelQuery : IRequest<IEnumerable<RoomDto>>
{
    public int HotelId { get; set; }
}