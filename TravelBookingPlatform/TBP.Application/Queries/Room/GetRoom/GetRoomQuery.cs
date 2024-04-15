using MediatR;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.Queries;

public class GetRoomQuery : IRequest<Room>
{
    public int RoomId { get; set; }
}