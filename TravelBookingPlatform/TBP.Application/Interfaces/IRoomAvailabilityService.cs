namespace TravelBookingPlatform.Application.Interfaces;

public interface IRoomAvailabilityService
{
    Task<bool> IsRoomAvailable(int roomId, DateTime checkInDate, DateTime checkOutDate);
}