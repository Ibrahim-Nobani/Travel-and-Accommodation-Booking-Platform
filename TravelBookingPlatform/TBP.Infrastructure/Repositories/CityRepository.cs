using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly TravelBookingPlatformDbContext _dbContext;

    public CityRepository(TravelBookingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<City> GetByIdAsync(int cityId)
    {
        return await _dbContext.Cities.FindAsync(cityId);
    }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        return await _dbContext.Cities.ToListAsync();
    }

    public void AddAsync(City city)
    {
        _dbContext.Cities.Add(city);
    }

    public void UpdateAsync(City city)
    {
        _dbContext.Update(city);
    }

    public void DeleteAsync(City city)
    {
        _dbContext.Cities.Remove(city);
    }

    public async Task<bool> CheckIfExistsByIdAsync(int cityId)
    {
        return await _dbContext.Cities.AnyAsync(c => c.Id == cityId);
    }
}