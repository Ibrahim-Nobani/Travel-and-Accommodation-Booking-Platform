using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
namespace TravelBookingPlatform.Application.Queries;

public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, Room>
{
    private readonly IRoomRepository _roomRepository;
    public GetRoomQueryHandler(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Room> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.RoomId);

        if (room == null)
        {
            throw new EntityNotFoundException(nameof(room));
        }

        return room;
    }
}