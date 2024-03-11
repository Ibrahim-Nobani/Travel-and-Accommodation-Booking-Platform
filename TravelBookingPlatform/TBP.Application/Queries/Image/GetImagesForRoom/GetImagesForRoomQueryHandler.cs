using MediatR;
using Microsoft.IdentityModel.Tokens;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Application.DTOs;
using AutoMapper;
namespace TravelBookingPlatform.Application.Queries;

public class GetImagesForRoomQueryHandler : IRequestHandler<GetImagesForRoomQuery, IEnumerable<ImageDto>>
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public GetImagesForRoomQueryHandler(IImageRepository imageRepository, IMapper mapper)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ImageDto>> Handle(GetImagesForRoomQuery request, CancellationToken cancellationToken)
    {
        var images = await _imageRepository.GetRoomImagesAsync(request.RoomId);

        if (images.IsNullOrEmpty())
        {
            throw new EntityNotFoundException(nameof(images));
        }

        var imagesDto = _mapper.Map<List<ImageDto>>(images);
        return imagesDto;
    }
}