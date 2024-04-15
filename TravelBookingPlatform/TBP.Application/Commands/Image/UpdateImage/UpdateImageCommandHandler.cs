using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Application.DTOs;

namespace TravelBookingPlatform.Application.Commands;

public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand, ImageDto>
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateImageCommandHandler(IImageRepository imageRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ImageDto> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
    {
        var image = await _imageRepository.GetByIdAsync(request.ImageId);

        if (image == null)
        {
            throw new EntityNotFoundException(nameof(image));
        }

        _mapper.Map(request.UpdateImageDto, image);

        _imageRepository.UpdateAsync(image);
        await _unitOfWork.SaveChangesAsync();

        var imageDto = _mapper.Map<ImageDto>(image);
        return imageDto;
    }
}