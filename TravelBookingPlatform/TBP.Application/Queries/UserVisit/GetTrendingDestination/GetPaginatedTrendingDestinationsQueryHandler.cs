using MediatR;
using Microsoft.IdentityModel.Tokens;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Application.DTOs;
using AutoMapper;
namespace TravelBookingPlatform.Application.Queries;

public class GetPaginatedTrendingDestinationsQueryHandler : IRequestHandler<GetPaginatedTrendingDestinationsQuery, IEnumerable<CityDto>>
{
    private readonly IUserVisitRepository _userVisitRepository;
    private readonly IMapper _mapper;

    public GetPaginatedTrendingDestinationsQueryHandler(IUserVisitRepository userVisitRepository, IMapper mapper)
    {
        _userVisitRepository = userVisitRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CityDto>> Handle(GetPaginatedTrendingDestinationsQuery request, CancellationToken cancellationToken)
    {
        var trendingDestinationCities = await _userVisitRepository.GetPaginatedTrendingDestinations(request.PaginationParameters);

        if (trendingDestinationCities.IsNullOrEmpty())
        {
            throw new EntityNotFoundException("Trending Destination Cities");
        }

        var citiesDto = _mapper.Map<List<CityDto>>(trendingDestinationCities);
        return citiesDto;
    }
}