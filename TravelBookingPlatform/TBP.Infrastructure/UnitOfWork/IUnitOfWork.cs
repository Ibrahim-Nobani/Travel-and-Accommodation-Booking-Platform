namespace TravelBookingPlatform.Infrastructure.Database;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}