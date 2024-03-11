using Microsoft.EntityFrameworkCore;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly TravelBookingPlatformDbContext _dbContext;

    public BookingRepository(TravelBookingPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Booking> GetByIdAsync(int bookingId)
    {
        return await _dbContext.Bookings.FindAsync(bookingId);
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await _dbContext.Bookings.ToListAsync();
    }

    public void AddAsync(Booking booking)
    {
        _dbContext.Bookings.Add(booking);
    }

    public void UpdateAsync(Booking booking)
    {
        _dbContext.Update(booking);
    }

    public void DeleteAsync(Booking booking)
    {
        _dbContext.Bookings.Remove(booking);
    }

    public async Task<IEnumerable<Booking>> GetBookingsForRoom(int roomId)
    {
        return await _dbContext.Bookings.Where(b => b.RoomId == roomId).ToListAsync();
    }

    public async Task<Booking> GetByIdIncludingUserAsync(int bookingId)
    {
        return await _dbContext.Bookings.Include(b => b.User).FirstOrDefaultAsync(b => b.Id == bookingId);
    }

    public async Task<Booking> GetByIdIncludingPaymentTransactionAsync(int bookingId)
    {
        return await _dbContext.Bookings.Include(b => b.PaymentTransaction).FirstOrDefaultAsync(b => b.Id == bookingId);
    }
}