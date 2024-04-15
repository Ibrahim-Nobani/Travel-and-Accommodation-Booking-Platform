using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Infrastructure.Repositories;

public class UserVisitRepository : IUserVisitRepository
{
    private readonly TravelBookingPlatformDbContext _dbContext;

    public UserVisitRepository(TravelBookingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserVisit> GetByIdAsync(int userVisitId)
    {
        return await _dbContext.UserVisits.FindAsync(userVisitId);
    }

    public async Task<IEnumerable<UserVisit>> GetAllAsync()
    {
        return await _dbContext.UserVisits.ToListAsync();
    }

    public void AddAsync(UserVisit userVisit)
    {
        _dbContext.UserVisits.Add(userVisit);
    }

    public void UpdateAsync(UserVisit userVisit)
    {
        _dbContext.Update(userVisit);
    }

    public void DeleteAsync(UserVisit userVisit)
    {
        _dbContext.UserVisits.Remove(userVisit);
    }

    public async Task<List<RecentlyVisitedHotelView>> GetPaginatedRecentlyVisitedHotels(int userId, PaginationParameters paginationParameters)
    {
        return await _dbContext.RecentlyVisitedHotels
            .Where(uv => uv.UserId == userId)
            .OrderByDescending(uv => uv.VisitDateTime)
            .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
            .Take(paginationParameters.PageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<City>> GetPaginatedTrendingDestinations(PaginationParameters paginationParameters)
    {
        var trendingDestinations = await _dbContext.Set<UserVisit>()
            .GroupBy(uv => uv.Hotel.City)
            .OrderByDescending(group => group.Count())
            .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
            .Take(paginationParameters.PageSize)
            .Select(group => group.Key)
            .ToListAsync();

        return trendingDestinations;
    }
}