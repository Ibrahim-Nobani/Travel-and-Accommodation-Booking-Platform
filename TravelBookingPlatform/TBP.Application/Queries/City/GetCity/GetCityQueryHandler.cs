using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
namespace TravelBookingPlatform.Application.Queries;

public class GetCityQueryHandler : IRequestHandler<GetCityQuery, CityDto>
{
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;

    public GetCityQueryHandler(ICityRepository cityRepository, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<CityDto> Handle(GetCityQuery request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(request.CityId);

        if (city == null)
        {
            throw new EntityNotFoundException(nameof(city));
        }

        var cityDto = _mapper.Map<CityDto>(city);
        return cityDto;
    }
}