using MediatR;
using Microsoft.IdentityModel.Tokens;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
namespace TravelBookingPlatform.Application.Queries;

public class GetFeaturedDealQueryHandler : IRequestHandler<GetFeaturedDealQuery, FeaturedDealView>
{
    private readonly IFeaturedDealRepository _featuredDealRepository;

    public GetFeaturedDealQueryHandler(IFeaturedDealRepository featuredDealRepository)
    {
        _featuredDealRepository = featuredDealRepository;
    }

    public async Task<FeaturedDealView> Handle(GetFeaturedDealQuery request, CancellationToken cancellationToken)
    {
        var featuredDeal = await _featuredDealRepository.GetFeaturedDealViewByIdAsync(request.FeaturedDealId);
        
        if (featuredDeal is null)
        {
            throw new EntityNotFoundException(nameof(featuredDeal));
        }

        return featuredDeal;
    }
}
