using Microsoft.EntityFrameworkCore;
namespace TravelBookingPlatform.Infrastructure.Database;
public class UnitOfWork : IUnitOfWork
{
    private readonly TravelBookingPlatformDbContext _dbContext;

    public UnitOfWork(TravelBookingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
