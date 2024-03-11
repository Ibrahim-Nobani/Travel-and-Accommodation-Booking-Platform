using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.Interfaces;

public interface IBookingRepository : IRepository<Booking>
{
    Task<IEnumerable<Booking>> GetBookingsForRoom(int roomId);
    Task<Booking> GetByIdIncludingUserAsync(int bookingId);
    Task<Booking> GetByIdIncludingPaymentTransactionAsync(int bookingId);
}