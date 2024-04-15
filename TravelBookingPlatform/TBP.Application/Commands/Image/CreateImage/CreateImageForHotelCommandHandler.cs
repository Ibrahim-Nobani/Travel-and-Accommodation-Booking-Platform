using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Application.Commands;

public class CreateImageForHotelCommandHandler : IRequestHandler<CreateImageForHotelCommand, ImageDto>
{
    private readonly IImageRepository _imageRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateImageForHotelCommandHandler(IImageRepository imageRepository, IMapper mapper, IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
    {
        _imageRepository = imageRepository;
        _hotelRepository = hotelRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ImageDto> Handle(CreateImageForHotelCommand request, CancellationToken cancellationToken)
    {
        var hotelExists = await _hotelRepository.CheckIfExistsByIdAsync(request.HotelId);

        if (!hotelExists)
        {
            throw new EntityNotFoundException(nameof(Hotel));
        }

        var image = _mapper.Map<Image>(request.CreateImageDto);
        image.ImageType = ImageType.Hotel;
        image.EntityId = request.HotelId;

        _imageRepository.AddAsync(image);
        await _unitOfWork.SaveChangesAsync();

        var imageDto = _mapper.Map<ImageDto>(image);
        return imageDto;
    }
}