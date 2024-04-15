using MediatR;
namespace TravelBookingPlatform.Application.Commands;

public class DeleteFeaturedDealCommand : IRequest<Task>
{
    public int FeaturedDealId { get; set; }
}