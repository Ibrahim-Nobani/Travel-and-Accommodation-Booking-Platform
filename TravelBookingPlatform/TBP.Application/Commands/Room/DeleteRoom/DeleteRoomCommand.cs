using MediatR;
namespace TravelBookingPlatform.Application.Commands;

public class DeleteRoomCommand : IRequest<Task>
{
    public int RoomId { get; set; }
}