using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
using TravelBookingPlatform.Application.DTOs;

namespace TravelBookingPlatform.Application.Commands;

public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, HotelDto>
{
    private readonly IHotelRepository _hotelRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateHotelCommandHandler(IHotelRepository hotelRepository, ICityRepository cityRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _hotelRepository = hotelRepository;
        _cityRepository = cityRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<HotelDto> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
    {
        var cityExists = await _cityRepository.CheckIfExistsByIdAsync(request.CityId);

        if (!cityExists)
        {
            throw new EntityNotFoundException(nameof(City));
        }

        var hotel = _mapper.Map<Hotel>(request.CreateHotelDto);
        hotel.CityId = request.CityId;
        hotel.CreatedAt = DateTime.UtcNow;

        _hotelRepository.AddAsync(hotel);
        await _unitOfWork.SaveChangesAsync();

        var hotelDto = _mapper.Map<HotelDto>(hotel);
        return hotelDto;
    }
}