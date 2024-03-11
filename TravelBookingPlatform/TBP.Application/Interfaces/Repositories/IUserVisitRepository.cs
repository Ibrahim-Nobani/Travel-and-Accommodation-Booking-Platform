using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.Interfaces;

public interface IUserVisitRepository : IRepository<UserVisit>
{
    Task<List<RecentlyVisitedHotelView>> GetPaginatedRecentlyVisitedHotels(int userId, PaginationParameters paginationParameters);
    Task<IEnumerable<City>> GetPaginatedTrendingDestinations(PaginationParameters paginationParameters);
}