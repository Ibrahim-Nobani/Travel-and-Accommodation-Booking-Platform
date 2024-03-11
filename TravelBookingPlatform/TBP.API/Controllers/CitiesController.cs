using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Commands;
using TravelBookingPlatform.Application.Queries;
namespace TravelBookingPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCities()
    {
        var getCitiesAdminQuery = new GetAllCitiesQuery();

        var cities = await _mediator.Send(getCitiesAdminQuery);
        return Ok(cities);
    }

    [HttpGet("{cityId}")]
    public async Task<IActionResult> GetCity([FromRoute] int cityId)
    {
        var getCityAdminQuery = new GetCityQuery
        {
            CityId = cityId
        };

        var city = await _mediator.Send(getCityAdminQuery);
        return Ok(city);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCity([FromBody] CreateCityDto createCityDto)
    {
        var createCityAdminCommand = new CreateCityCommand
        {
            CreateCityDto = createCityDto
        };

        var city = await _mediator.Send(createCityAdminCommand);
        return Ok(city);
    }

    [HttpDelete("{cityId}")]
    public async Task<IActionResult> DeleteCity([FromRoute] int cityId)
    {
        var deleteCityAdminCommand = new DeleteCityCommand
        {
            CityId = cityId
        };

        await _mediator.Send(deleteCityAdminCommand);
        return NoContent();
    }

    [HttpPut("{cityId}")]
    public async Task<IActionResult> UpdateCity([FromRoute] int cityId, [FromBody] UpdateCityDto updateCityDto)
    {
        var updateCityAdminCommand = new UpdateCityCommand
        {
            CityId = cityId,
            UpdateCityDto = updateCityDto
        };

        var city = await _mediator.Send(updateCityAdminCommand);
        return Ok(city);
    }

    [HttpGet("/trending-destinations")]
    public async Task<IActionResult> GetTrendingDestination([FromQuery] PaginationParameters paginationParameters)
    {
        var getTrendingDestinationQuery = new GetPaginatedTrendingDestinationsQuery
        {
            PaginationParameters = paginationParameters
        };

        var trendingDestinations = await _mediator.Send(getTrendingDestinationQuery);
        return Ok(trendingDestinations);
    }

    [HttpGet("{cityId}/hotels")]
    public async Task<IActionResult> GetHotels([FromRoute] int cityId)
    {
        var getAllHotelsForCityQuery = new GetAllHotelsForCityQuery
        {
            CityId = cityId
        };

        var hotels = await _mediator.Send(getAllHotelsForCityQuery);
        return Ok(hotels);
    }

    [HttpPost("{cityId}/hotels")]
    public async Task<IActionResult> CreateHotelForCity([FromBody] CreateHotelDto createHotelDto, [FromRoute] int cityId)
    {
        var createHotelAdminCommand = new CreateHotelCommand
        {
            CreateHotelDto = createHotelDto,
            CityId = cityId
        };

        var hotel = await _mediator.Send(createHotelAdminCommand);
        return Ok(hotel);
    }
}