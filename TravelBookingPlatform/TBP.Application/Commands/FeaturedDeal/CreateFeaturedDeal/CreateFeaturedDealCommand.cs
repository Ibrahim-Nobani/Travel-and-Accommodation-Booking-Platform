using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Commands;

public class CreateFeaturedDealCommand : IRequest<FeaturedDealDto>
{
    public CreateFeaturedDealDto CreateFeaturedDealDto { get; set; }
}