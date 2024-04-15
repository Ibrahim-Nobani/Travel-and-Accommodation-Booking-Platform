using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Repositories;
namespace TravelBookingPlatform.Application.Queries;

public class GetPaginatedUserRecentlyVisitedHotelsQueryHandler : IRequestHandler<GetPaginatedUserRecentlyVisitedHotelsQuery, IEnumerable<RecentlyVisitedHotelDto>>
{
    private readonly IUserVisitRepository _userVisitRepository;
    private readonly IUserRepository _userRepository;

    private readonly IMapper _mapper;

    public GetPaginatedUserRecentlyVisitedHotelsQueryHandler(IUserVisitRepository userVisitRepository, IUserRepository userRepository, IMapper mapper)
    {
        _userVisitRepository = userVisitRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RecentlyVisitedHotelDto>> Handle(GetPaginatedUserRecentlyVisitedHotelsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            throw new EntityNotFoundException(nameof(user));
        }

        var recentlyVisitedHotels = await _userVisitRepository.GetPaginatedRecentlyVisitedHotels(request.UserId, request.PaginationParameters);

        if (recentlyVisitedHotels.IsNullOrEmpty())
        {
            throw new EntityNotFoundException(nameof(recentlyVisitedHotels));
        }

        var recentlyVisitedHotelsDto = _mapper.Map<List<RecentlyVisitedHotelDto>>(recentlyVisitedHotels);

        return recentlyVisitedHotelsDto;
    }
}