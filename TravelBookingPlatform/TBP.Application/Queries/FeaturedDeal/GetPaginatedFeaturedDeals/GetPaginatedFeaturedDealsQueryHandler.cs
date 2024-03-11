using MediatR;
using Microsoft.IdentityModel.Tokens;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
namespace TravelBookingPlatform.Application.Queries;

public class GetPaginatedFeaturedDealsQueryHandler : IRequestHandler<GetPaginatedFeaturedDealsQuery, IEnumerable<FeaturedDealView>>
{
    private readonly IFeaturedDealRepository _featuredDealRepository;

    public GetPaginatedFeaturedDealsQueryHandler(IFeaturedDealRepository featuredDealRepository)
    {
        _featuredDealRepository = featuredDealRepository;
    }

    public async Task<IEnumerable<FeaturedDealView>> Handle(GetPaginatedFeaturedDealsQuery request, CancellationToken cancellationToken)
    {
        var featuredDeals = await _featuredDealRepository.GetPaginatedFeaturedDealsViewsAsync(request.PaginationParameters);
        
        if (featuredDeals.IsNullOrEmpty())
        {
            throw new EntityNotFoundException(nameof(featuredDeals));
        }

        return featuredDeals;
    }
}
