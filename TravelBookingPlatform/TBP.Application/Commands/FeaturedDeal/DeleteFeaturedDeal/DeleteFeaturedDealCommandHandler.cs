using MediatR;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Application.Commands;

public class DeleteFeaturedDealCommandHandler : IRequestHandler<DeleteFeaturedDealCommand, Task>
{
    private readonly IFeaturedDealRepository _featuredDealRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFeaturedDealCommandHandler(IFeaturedDealRepository featuredDealRepository, IUnitOfWork unitOfWork)
    {
        _featuredDealRepository = featuredDealRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Task> Handle(DeleteFeaturedDealCommand request, CancellationToken cancellationToken)
    {
        var featuredDeal = await _featuredDealRepository.GetByIdAsync(request.FeaturedDealId);

        if (featuredDeal == null)
        {
            throw new EntityNotFoundException(nameof(featuredDeal));
        }

        _featuredDealRepository.DeleteAsync(featuredDeal);
        await _unitOfWork.SaveChangesAsync();

        return Task.CompletedTask;
    }
}