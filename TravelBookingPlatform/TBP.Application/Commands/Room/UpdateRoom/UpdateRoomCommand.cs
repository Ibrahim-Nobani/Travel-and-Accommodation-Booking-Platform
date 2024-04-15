using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Commands;

public class UpdateRoomCommand : IRequest<RoomDto>
{
    public int RoomId { get; set; }
    public UpdateRoomDto UpdateRoomDto { get; set; }
}