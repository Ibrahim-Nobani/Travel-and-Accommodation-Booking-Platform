using MediatR;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.Queries;

public class GetPaginatedTrendingDestinationsQuery : IRequest<IEnumerable<CityDto>>
{
    public PaginationParameters PaginationParameters { get; set; }
}