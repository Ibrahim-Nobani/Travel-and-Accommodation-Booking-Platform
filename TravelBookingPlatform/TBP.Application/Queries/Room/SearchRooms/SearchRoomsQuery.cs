using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class SearchRoomsQuery : IRequest<IEnumerable<RoomDto>>
{
    public SearchRoomCriteria SearchRoom { get; set; }
}