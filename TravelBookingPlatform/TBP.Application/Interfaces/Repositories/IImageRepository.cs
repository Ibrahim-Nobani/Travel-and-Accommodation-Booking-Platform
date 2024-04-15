using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.Interfaces;

public interface IImageRepository : IRepository<Image>
{
    Task<IEnumerable<Image>> GetRoomImagesAsync(int roomId);
    Task<IEnumerable<Image>> GetHotelImagesAsync(int hotelId);
}