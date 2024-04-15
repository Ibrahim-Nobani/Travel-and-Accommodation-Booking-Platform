using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
namespace TravelBookingPlatform.Application.Queries;

public class GetAvailableRoomsForHotelQueryHandler : IRequestHandler<GetAvailableRoomsForHotelQuery, IEnumerable<RoomDto>>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;

    public GetAvailableRoomsForHotelQueryHandler(IRoomRepository roomRepository, IHotelRepository hotelRepository, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoomDto>> Handle(GetAvailableRoomsForHotelQuery request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);

        if (hotel == null)
        {
            throw new EntityNotFoundException(nameof(hotel));
        }

        var rooms = await _roomRepository.GetAvailableRoomsForHotel(request.HotelId);

        if (rooms.IsNullOrEmpty())
        {
            throw new EntityNotFoundException(nameof(rooms));
        }

        var roomsDto = _mapper.Map<List<RoomDto>>(rooms);

        return roomsDto;
    }
}