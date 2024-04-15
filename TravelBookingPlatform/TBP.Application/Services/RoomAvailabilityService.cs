using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Application.Interfaces;

namespace TravelBookingPlatform.Application.Services;

public class RoomAvailabilityService : IRoomAvailabilityService
{
    private readonly IBookingRepository _bookingRepository;

    public RoomAvailabilityService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    public async Task<bool> IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate)
    {
        var existingBookings = await _bookingRepository.GetBookingsForRoom(roomId);

        var activeBookings = existingBookings.Where(b => b.Status != BookingStatus.Cancelled);

        return !activeBookings.Any(b =>
            (checkInDate >= b.CheckInDate && checkInDate < b.CheckOutDate) ||
            (checkOutDate > b.CheckInDate && checkOutDate <= b.CheckOutDate) ||
            (checkInDate <= b.CheckInDate && checkOutDate >= b.CheckOutDate));
    }
}