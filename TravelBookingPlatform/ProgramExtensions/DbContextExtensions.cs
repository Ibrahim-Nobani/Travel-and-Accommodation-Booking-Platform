using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Infrastructure.Database;

public static class DbContextExtensions
{
    public static void AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TravelBookingPlatformDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlServerConnectionString"));
        });
    }
}
