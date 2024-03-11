using MediatR;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.Queries;

public class GetPaginatedFeaturedDealsQuery : IRequest<IEnumerable<FeaturedDealView>>
{
    public PaginationParameters PaginationParameters { get; set; }
}