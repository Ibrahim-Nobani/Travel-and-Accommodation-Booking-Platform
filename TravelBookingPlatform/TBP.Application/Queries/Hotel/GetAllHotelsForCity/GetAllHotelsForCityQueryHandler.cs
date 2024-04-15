using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
namespace TravelBookingPlatform.Application.Queries;

public class GetAllHotelsForCityQueryHandler : IRequestHandler<GetAllHotelsForCityQuery, IEnumerable<HotelDto>>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;

    public GetAllHotelsForCityQueryHandler(IHotelRepository hotelRepository, ICityRepository cityRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _cityRepository = cityRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<HotelDto>> Handle(GetAllHotelsForCityQuery request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(request.CityId);

        if (city == null)
        {
            throw new EntityNotFoundException(nameof(city));
        }

        var hotels = await _hotelRepository.GetHotelsForCity(request.CityId);

        if (hotels.IsNullOrEmpty())
        {
            throw new EntityNotFoundException(nameof(hotels));
        }

        var hotelsDto = _mapper.Map<List<HotelDto>>(hotels);

        return hotelsDto;
    }
}