using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Application.Commands;

public class CreateFeaturedDealCommandHandler : IRequestHandler<CreateFeaturedDealCommand, FeaturedDealDto>
{
    private readonly IFeaturedDealRepository _featuredDealRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateFeaturedDealCommandHandler(IFeaturedDealRepository featuredDealRepository, IRoomRepository roomRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _featuredDealRepository = featuredDealRepository;
        _roomRepository = roomRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<FeaturedDealDto> Handle(CreateFeaturedDealCommand request, CancellationToken cancellationToken)
    {
        var roomExists = await _roomRepository.CheckIfExistsByIdAsync(request.CreateFeaturedDealDto.RoomId);

        if (!roomExists)
        {
            throw new EntityNotFoundException(nameof(Room));
        }

        var isRoomAlreadyFeatured = await _featuredDealRepository.IsRoomAlreadyFeaturedDealAsync(request.CreateFeaturedDealDto.RoomId);

        if (isRoomAlreadyFeatured)
        {
            throw new RoomAlreadyFeaturedException();
        }

        var featuredDeal = _mapper.Map<FeaturedDeal>(request.CreateFeaturedDealDto);
        featuredDeal.CreatedAt = DateTime.UtcNow;


        _featuredDealRepository.AddAsync(featuredDeal);
        await _unitOfWork.SaveChangesAsync();

        var featuredDealDto = _mapper.Map<FeaturedDealDto>(featuredDeal);
        return featuredDealDto;
    }
}