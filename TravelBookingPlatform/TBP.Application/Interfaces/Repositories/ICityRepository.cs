using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.Interfaces;

public interface ICityRepository : IRepository<City>
{
    Task<bool> CheckIfExistsByIdAsync(int cityId);
}