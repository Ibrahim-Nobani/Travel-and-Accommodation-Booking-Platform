using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Application.Commands;

public class UpdateFeaturedDealCommandHandler : IRequestHandler<UpdateFeaturedDealCommand, FeaturedDealDto>
{
    private readonly IFeaturedDealRepository _featuredDealRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateFeaturedDealCommandHandler(IFeaturedDealRepository featuredDealRepository, IRoomRepository roomRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _featuredDealRepository = featuredDealRepository;
        _roomRepository = roomRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<FeaturedDealDto> Handle(UpdateFeaturedDealCommand request, CancellationToken cancellationToken)
    {
        var roomExists = await _roomRepository.CheckIfExistsByIdAsync(request.UpdateFeaturedDealDto.RoomId);

        if (!roomExists)
        {
            throw new EntityNotFoundException(nameof(Room));
        }

        var featuredDeal = await _featuredDealRepository.GetByIdAsync(request.FeaturedDealId);

        if (featuredDeal is null)
        {
            throw new EntityNotFoundException(nameof(featuredDeal));
        }

        var isRoomAlreadyFeatured = await _featuredDealRepository.IsRoomAlreadyFeaturedDealAsync(request.UpdateFeaturedDealDto.RoomId);

        if (isRoomAlreadyFeatured)
        {
            throw new RoomAlreadyFeaturedException();
        }

        featuredDeal.UpdatedAt = DateTime.UtcNow;
        _mapper.Map(request.UpdateFeaturedDealDto, featuredDeal);

        _featuredDealRepository.UpdateAsync(featuredDeal);
        await _unitOfWork.SaveChangesAsync();

        var featuredDealDto = _mapper.Map<FeaturedDealDto>(featuredDeal);
        return featuredDealDto;
    }
}