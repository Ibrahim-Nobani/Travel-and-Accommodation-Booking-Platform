using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
namespace TravelBookingPlatform.Application.Queries;

public class GetHotelQueryHandler : IRequestHandler<GetHotelQuery, HotelDto>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly IMapper _mapper;
    public GetHotelQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
    {
        _hotelRepository = hotelRepository;
        _mapper = mapper;
    }

    public async Task<HotelDto> Handle(GetHotelQuery request, CancellationToken cancellationToken)
    {
        var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);

        if (hotel == null)
        {
            throw new EntityNotFoundException(nameof(hotel));
        }

        var hotelDto = _mapper.Map<HotelDto>(hotel);
        return hotelDto;
    }
}