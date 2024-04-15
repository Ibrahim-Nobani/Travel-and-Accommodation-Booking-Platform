using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Domain.Enums;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Application.Commands;

public class CreateImageForRoomCommandHandler : IRequestHandler<CreateImageForRoomCommand, ImageDto>
{
    private readonly IImageRepository _imageRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateImageForRoomCommandHandler(IImageRepository imageRepository, IMapper mapper, IRoomRepository roomRepository, IUnitOfWork unitOfWork)
    {
        _imageRepository = imageRepository;
        _roomRepository = roomRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ImageDto> Handle(CreateImageForRoomCommand request, CancellationToken cancellationToken)
    {
        var roomExists = await _roomRepository.CheckIfExistsByIdAsync(request.RoomId);

        if (!roomExists)
        {
            throw new EntityNotFoundException(nameof(Room));
        }

        var image = _mapper.Map<Image>(request.CreateImageDto);
        image.ImageType = ImageType.Room;
        image.EntityId = request.RoomId;

        _imageRepository.AddAsync(image);
        await _unitOfWork.SaveChangesAsync();

        var imageDto = _mapper.Map<ImageDto>(image);
        return imageDto;
    }
}