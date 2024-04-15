using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Infrastructure.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly TravelBookingPlatformDbContext _dbContext;

    public HotelRepository(TravelBookingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Hotel> GetByIdAsync(int hotelId)
    {
        return await _dbContext.Hotels.Include(h => h.Rooms).FirstOrDefaultAsync(h => h.Id == hotelId);
    }

    public async Task<IEnumerable<Hotel>> GetAllAsync()
    {
        return await _dbContext.Hotels.Include(h => h.Rooms).ToListAsync();
    }

    public void AddAsync(Hotel hotel)
    {
        _dbContext.Hotels.Add(hotel);
    }

    public void UpdateAsync(Hotel hotel)
    {
        _dbContext.Update(hotel);
    }

    public void DeleteAsync(Hotel hotel)
    {
        _dbContext.Hotels.Remove(hotel);
    }

    public async Task<IEnumerable<Hotel>> GetHotelsForCity(int cityId)
    {
        return await _dbContext.Hotels.Where(h => h.CityId == cityId).ToListAsync();
    }

    public async Task<IEnumerable<Hotel>> SearchAsync(SearchHotelCriteria searchHotelCriteria)
    {
        var query = _dbContext.Hotels
            .Include(h => h.City)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchHotelCriteria.Name))
        {
            query = query.Where(h => h.Name.Contains(searchHotelCriteria.Name));
        }

        if (searchHotelCriteria.StarRating.HasValue)
        {
            query = query.Where(h => h.StarRating == searchHotelCriteria.StarRating);
        }

        if (!string.IsNullOrEmpty(searchHotelCriteria.Location))
        {
            query = query.Where(h => h.Location.Contains(searchHotelCriteria.Location));
        }

        if (!string.IsNullOrEmpty(searchHotelCriteria.CityName))
        {
            query = query.Where(h => h.City.Name.Contains(searchHotelCriteria.CityName));
        }

        if (!string.IsNullOrEmpty(searchHotelCriteria.Owner))
        {
            query = query.Where(h => h.Owner.Contains(searchHotelCriteria.Owner));
        }

        return await query.ToListAsync();
    }

    public async Task<bool> CheckIfExistsByIdAsync(int hotelId)
    {
        return await _dbContext.Hotels.AnyAsync(h => h.Id == hotelId);
    }
}