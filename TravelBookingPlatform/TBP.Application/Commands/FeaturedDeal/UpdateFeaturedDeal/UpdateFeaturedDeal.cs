using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Commands;

public class UpdateFeaturedDealCommand : IRequest<FeaturedDealDto>
{
    public int FeaturedDealId { get; set; }
    public UpdateFeaturedDealDto UpdateFeaturedDealDto { get; set; }
}