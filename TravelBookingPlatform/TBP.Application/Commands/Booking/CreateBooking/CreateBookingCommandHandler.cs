using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Domain.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Commands;

public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingDto>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IPricingService _pricingService;
    private readonly IRoomAvailabilityService _roomAvailabilityService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateBookingCommandHandler(IBookingRepository bookingRepository, IRoomRepository roomRepository, IRoomAvailabilityService roomAvailabilityService, IUnitOfWork unitOfWork, IMapper mapper, IPricingService pricingService)
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
        _mapper = mapper;
        _pricingService = pricingService;
        _roomAvailabilityService = roomAvailabilityService;
        _unitOfWork = unitOfWork;
    }

    public async Task<BookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.CreateBookingDto.RoomId);
        if (room == null)
        {
            throw new EntityNotFoundException(nameof(room));
        }

        var roomAvailable = await _roomAvailabilityService.IsRoomAvailable(room.Id, request.CreateBookingDto.CheckInDate, request.CreateBookingDto.CheckOutDate);
        if (!roomAvailable)
        {
            throw new RoomNotAvailableException();
        }

        var booking = _mapper.Map<Booking>(request.CreateBookingDto);
        booking.TotalPrice = _pricingService.CalculateTotalPrice(room.Price, booking.CheckInDate, booking.CheckOutDate);
        booking.Status = BookingStatus.Pending;

        UpdateRoomAvailability(room, booking.CheckOutDate);
        _bookingRepository.AddAsync(booking);
        await _unitOfWork.SaveChangesAsync();
        
        var bookingDto = _mapper.Map<BookingDto>(booking);
        return bookingDto;
    }

    private void UpdateRoomAvailability(Room room, DateTime checkoutDate)
    {
        if (room.Availability)
        {
            if (checkoutDate >= DateTime.UtcNow)
            {
                room.Availability = false;
                _roomRepository.UpdateAsync(room);
            }
        }
    }
}