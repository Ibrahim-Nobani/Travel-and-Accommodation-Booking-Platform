using MediatR;
using TravelBookingPlatform.Domain.Entities;
namespace TravelBookingPlatform.Application.Queries;

public class GetFeaturedDealQuery:IRequest<FeaturedDealView>
{
    public int FeaturedDealId { get; set; }
}