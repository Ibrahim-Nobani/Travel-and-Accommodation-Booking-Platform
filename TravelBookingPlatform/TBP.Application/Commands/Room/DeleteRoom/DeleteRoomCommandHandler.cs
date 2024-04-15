using MediatR;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Application.Commands;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, Task>
{
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoomCommandHandler(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Task> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var room = await _roomRepository.GetByIdAsync(request.RoomId);

        if (room == null)
        {
            throw new EntityNotFoundException(nameof(room));
        }

        _roomRepository.DeleteAsync(room);
        await _unitOfWork.SaveChangesAsync();

        return Task.CompletedTask;
    }
}