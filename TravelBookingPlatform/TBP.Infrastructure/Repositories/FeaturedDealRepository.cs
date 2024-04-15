using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Infrastructure.Repositories;

public class FeaturedDealRepository : IFeaturedDealRepository
{
    private readonly TravelBookingPlatformDbContext _dbContext;

    public FeaturedDealRepository(TravelBookingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FeaturedDeal> GetByIdAsync(int featuredDealId)
    {
        return await _dbContext.FeaturedDeals.FindAsync(featuredDealId);
    }

    public async Task<IEnumerable<FeaturedDeal>> GetAllAsync()
    {
        return await _dbContext.FeaturedDeals.ToListAsync();
    }

    public void AddAsync(FeaturedDeal featuredDeal)
    {
        _dbContext.FeaturedDeals.Add(featuredDeal);
    }

    public void UpdateAsync(FeaturedDeal featuredDeal)
    {
        _dbContext.Update(featuredDeal);
    }

    public void DeleteAsync(FeaturedDeal featuredDeal)
    {
        _dbContext.FeaturedDeals.Remove(featuredDeal);
    }

    public async Task<IEnumerable<FeaturedDealView>> GetPaginatedFeaturedDealsViewsAsync(PaginationParameters paginationParameters)
    {
        return await _dbContext.FeaturedDealView
                .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize).ToListAsync();
    }

    public async Task<FeaturedDealView> GetFeaturedDealViewByIdAsync(int featuredDealId)
    {
        return await _dbContext.FeaturedDealView
                .FirstOrDefaultAsync(fd => fd.FeaturedDealId == featuredDealId);
    }

    public async Task<bool> IsRoomAlreadyFeaturedDealAsync(int roomId)
    {
        return await _dbContext.FeaturedDeals
                .AnyAsync(fd => fd.RoomId == roomId);
    }
}