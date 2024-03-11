using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Application.Commands;

public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CityDto>
{
    private readonly ICityRepository _cityRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCityCommandHandler(ICityRepository cityRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CityDto> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var city = _mapper.Map<City>(request.CreateCityDto);
        city.CreatedAt = DateTime.UtcNow;

        _cityRepository.AddAsync(city);
        await _unitOfWork.SaveChangesAsync();

        var cityDto = _mapper.Map<CityDto>(city);
        return cityDto;
    }
}