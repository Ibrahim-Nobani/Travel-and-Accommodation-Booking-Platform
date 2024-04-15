using MediatR;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;

namespace TravelBookingPlatform.Application.Commands;

public class DeleteHotelAdminCommandHandler : IRequestHandler<DeleteHotelAdminCommand, Task>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteHotelAdminCommandHandler(IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
    {
        _hotelRepository = hotelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Task> Handle(DeleteHotelAdminCommand request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);

        if (hotel == null)
        {
            throw new EntityNotFoundException(nameof(hotel));
        }

        _hotelRepository.DeleteAsync(hotel);
        await _unitOfWork.SaveChangesAsync();

        return Task.CompletedTask;
    }
}