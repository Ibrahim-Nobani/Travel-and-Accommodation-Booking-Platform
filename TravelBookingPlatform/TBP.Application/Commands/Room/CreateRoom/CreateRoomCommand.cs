using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Commands;

public class CreateRoomCommand : IRequest<RoomDto>
{
    public CreateRoomDto CreateRoomDto { get; set; }
    public int HotelId { get; set; }
}