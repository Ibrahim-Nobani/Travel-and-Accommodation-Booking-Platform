using MediatR;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;

namespace TravelBookingPlatform.Application.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Task>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Task> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new EntityNotFoundException(nameof(user));
        }

        _userRepository.DeleteAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return Task.CompletedTask;
    }
}