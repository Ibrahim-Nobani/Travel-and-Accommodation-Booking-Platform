using AutoMapper;
using MediatR;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Domain.Entities;

namespace TravelBookingPlatform.Application.Commands;

public class UpdateHotelAdminCommandHandler : IRequestHandler<UpdateHotelAdminCommand, HotelDto>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateHotelAdminCommandHandler(IHotelRepository hotelRepository, ICityRepository cityRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _hotelRepository = hotelRepository;
        _cityRepository = cityRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<HotelDto> Handle(UpdateHotelAdminCommand request, CancellationToken cancellationToken)
    {
        var cityExists = await _cityRepository.CheckIfExistsByIdAsync(request.UpdateHotelDto.CityId);

        if (!cityExists)
        {
            throw new EntityNotFoundException(nameof(City));
        }

        var hotel = await _hotelRepository.GetByIdAsync(request.HotelId);

        if (hotel == null)
        {
            throw new EntityNotFoundException(nameof(hotel));
        }

        hotel.UpdatedAt = DateTime.UtcNow;
        _mapper.Map(request.UpdateHotelDto, hotel);

        _hotelRepository.UpdateAsync(hotel);
        await _unitOfWork.SaveChangesAsync();

        var hotelDto = _mapper.Map<HotelDto>(hotel);
        return hotelDto;
    }
}