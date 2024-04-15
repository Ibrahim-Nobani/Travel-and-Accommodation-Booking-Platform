using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetAllRoomsQuery : IRequest<IEnumerable<RoomDto>> { }