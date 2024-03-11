using MediatR;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Queries;

public class GetPaginatedUserRecentlyVisitedHotelsQuery : IRequest<IEnumerable<RecentlyVisitedHotelDto>>
{
    public int UserId { get; set; }
    public PaginationParameters PaginationParameters { get; set; }
}