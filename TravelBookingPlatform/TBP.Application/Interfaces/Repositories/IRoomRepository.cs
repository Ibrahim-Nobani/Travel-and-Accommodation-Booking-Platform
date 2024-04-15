using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.Interfaces;

public interface IRoomRepository : IRepository<Room>
{
    Task<IEnumerable<Room>> GetRoomsForHotel(int hotelId);
    Task<IEnumerable<Room>> GetAvailableRoomsForHotel(int hotelId);
    Task<IEnumerable<Room>> SearchAsync(SearchRoomCriteria searchRoomCriteria);
    Task<bool> CheckIfExistsByIdAsync(int roomId);
}