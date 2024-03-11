using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Infrastructure.Database;

public class TravelBookingPlatformDbContext : DbContext
{
    public TravelBookingPlatformDbContext() { }

    public TravelBookingPlatformDbContext(DbContextOptions<TravelBookingPlatformDbContext> options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserVisit> UserVisits { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<FeaturedDeal> FeaturedDeals { get; set; }
    public DbSet<RecentlyVisitedHotelView> RecentlyVisitedHotels { get; set; }
    public DbSet<FeaturedDealView> FeaturedDealView { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecentlyVisitedHotelView>().HasNoKey().ToView(nameof(RecentlyVisitedHotels));
        modelBuilder.Entity<FeaturedDealView>().HasNoKey().ToView(nameof(FeaturedDealView));

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TravelBookingPlatformDbContext).Assembly);

        modelBuilder.Seed();
    }
}
