using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
namespace TravelBookingPlatform.Application.Queries;

public class SearchRoomsQueryHandler : IRequestHandler<SearchRoomsQuery, IEnumerable<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;

    public SearchRoomsQueryHandler(IRoomRepository roomRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoomDto>> Handle(SearchRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _roomRepository.SearchAsync(request.SearchRoom);

        if (rooms.IsNullOrEmpty())
        {
            throw new EntityNotFoundException(nameof(rooms));
        }

        var roomsDto = _mapper.Map<List<RoomDto>>(rooms);

        return roomsDto;
    }
}