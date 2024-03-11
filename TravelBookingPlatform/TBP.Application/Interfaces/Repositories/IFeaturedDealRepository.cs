using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.Interfaces;

public interface IFeaturedDealRepository : IRepository<FeaturedDeal>
{
    Task<IEnumerable<FeaturedDealView>> GetPaginatedFeaturedDealsViewsAsync(PaginationParameters paginationParameters);
    Task<bool> IsRoomAlreadyFeaturedDealAsync(int roomId);
    Task<FeaturedDealView> GetFeaturedDealViewByIdAsync(int featuredDealId);
}