using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
namespace TravelBookingPlatform.Application.Queries;

public class GetImageQueryHandler : IRequestHandler<GetImageQuery, ImageDto>
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public GetImageQueryHandler(IImageRepository imageRepository, IMapper mapper)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
    }

    public async Task<ImageDto> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var image = await _imageRepository.GetByIdAsync(request.ImageId);

        if (image == null)
        {
            throw new EntityNotFoundException(nameof(image));
        }

        var imageDto = _mapper.Map<ImageDto>(image);
        return imageDto;
    }
}