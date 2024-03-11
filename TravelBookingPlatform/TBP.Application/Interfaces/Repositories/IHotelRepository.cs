using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.Interfaces;

public interface IHotelRepository : IRepository<Hotel>
{
    Task<IEnumerable<Hotel>> SearchAsync(SearchHotelCriteria searchHotelCriteria);
    Task<IEnumerable<Hotel>> GetHotelsForCity(int cityId);
    Task<bool> CheckIfExistsByIdAsync(int hotelId);
}