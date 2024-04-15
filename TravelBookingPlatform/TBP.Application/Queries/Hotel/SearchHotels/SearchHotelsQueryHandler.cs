using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
namespace TravelBookingPlatform.Application.Queries;

public class SearchHotelsQueryHandler : IRequestHandler<SearchHotelsQuery, IEnumerable<HotelDto>>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;

    public SearchHotelsQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<HotelDto>> Handle(SearchHotelsQuery request, CancellationToken cancellationToken)
    {
        var hotels = await _hotelRepository.SearchAsync(request.SearchHotel);

        if (hotels.IsNullOrEmpty())
        {
            throw new EntityNotFoundException(nameof(hotels));
        }

        var hotelsDto = _mapper.Map<List<HotelDto>>(hotels);

        return hotelsDto;
    }
}