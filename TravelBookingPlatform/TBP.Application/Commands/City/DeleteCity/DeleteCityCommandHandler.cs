using MediatR;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Application.Commands;

public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, Task>
{
    private readonly ICityRepository _cityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCityCommandHandler(ICityRepository cityRepository, IUnitOfWork unitOfWork)
    {
        _cityRepository = cityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Task> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(request.CityId);

        if (city == null)
        {
            throw new EntityNotFoundException(nameof(city));
        }

        _cityRepository.DeleteAsync(city);
        await _unitOfWork.SaveChangesAsync();

        return Task.CompletedTask;
    }
}