using MediatR;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Application.Commands;

public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, Task>
{
    private readonly IImageRepository _imageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteImageCommandHandler(IImageRepository imageRepository, IUnitOfWork unitOfWork)
    {
        _imageRepository = imageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Task> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _imageRepository.GetByIdAsync(request.ImageId);

        if (image == null)
        {
            throw new EntityNotFoundException(nameof(image));
        }

        _imageRepository.DeleteAsync(image);
        await _unitOfWork.SaveChangesAsync();

        return Task.CompletedTask;
    }
}