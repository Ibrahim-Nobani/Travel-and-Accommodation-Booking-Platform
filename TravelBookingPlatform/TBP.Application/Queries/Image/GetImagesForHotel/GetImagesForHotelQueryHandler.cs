using MediatR;
using TravelBookingPlatform.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Application.DTOs;
using AutoMapper;
namespace TravelBookingPlatform.Application.Queries;

public class GetImagesForHotelQueryHandler : IRequestHandler<GetImagesForHotelQuery, IEnumerable<ImageDto>>
{
    private readonly IImageRepository _imageRepository;
    private readonly IMapper _mapper;

    public GetImagesForHotelQueryHandler(IImageRepository imageRepository, IMapper mapper)
    {
        _imageRepository = imageRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ImageDto>> Handle(GetImagesForHotelQuery request, CancellationToken cancellationToken)
    {
        var images = await _imageRepository.GetHotelImagesAsync(request.HotelId);

        if (images.IsNullOrEmpty())
        {
            throw new EntityNotFoundException(nameof(images));
        }

        var imagesDto = _mapper.Map<List<ImageDto>>(images);
        return imagesDto;
    }
}