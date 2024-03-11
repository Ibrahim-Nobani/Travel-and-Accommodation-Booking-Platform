using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Commands;

public class CreateRoomAdminHandler : IRequestHandler<CreateRoomCommand, RoomDto>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateRoomAdminHandler(IRoomRepository roomRepository, IHotelRepository hotelRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _roomRepository = roomRepository;
        _hotelRepository = hotelRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RoomDto> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var hotelExists = await _hotelRepository.CheckIfExistsByIdAsync(request.HotelId);

        if (!hotelExists)
        {
            throw new EntityNotFoundException(nameof(Hotel));
        }

        var room = _mapper.Map<Room>(request.CreateRoomDto);
        room.HotelId = request.HotelId;
        room.CreatedAt = DateTime.UtcNow;

        _roomRepository.AddAsync(room);
        await _unitOfWork.SaveChangesAsync();

        var roomDto = _mapper.Map<RoomDto>(room);
        return roomDto;
    }
}