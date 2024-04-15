using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetImagesForRoomQuery : IRequest<IEnumerable<ImageDto>>
{
    public int RoomId { get; set; }
}