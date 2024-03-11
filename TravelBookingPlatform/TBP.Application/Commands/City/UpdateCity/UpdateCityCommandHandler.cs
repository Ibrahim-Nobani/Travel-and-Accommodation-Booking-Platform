using AutoMapper;
using MediatR;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Exceptions;
using TravelBookingPlatform.Application.Interfaces;
using TravelBookingPlatform.Infrastructure.Database;
namespace TravelBookingPlatform.Application.Commands;

public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, CityDto>
{
    private readonly ICityRepository _cityRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCityCommandHandler(ICityRepository cityRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _cityRepository = cityRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<CityDto> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetByIdAsync(request.CityId);

        if (city == null)
        {
            throw new EntityNotFoundException(nameof(city));
        }

        city.UpdatedAt = DateTime.UtcNow;
        _mapper.Map(request.UpdateCityDto, city);

        _cityRepository.UpdateAsync(city);
        await _unitOfWork.SaveChangesAsync();

        var cityDto = _mapper.Map<CityDto>(city);

        return cityDto;
    }
}