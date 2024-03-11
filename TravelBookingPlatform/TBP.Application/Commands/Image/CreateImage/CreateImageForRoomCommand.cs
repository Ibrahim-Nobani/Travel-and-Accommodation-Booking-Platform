using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Commands;

public class CreateImageForRoomCommand : IRequest<ImageDto>
{
    public int RoomId { get; set; }
    public CreateImageDto CreateImageDto { get; set; }
}