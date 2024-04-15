using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Commands;
using TravelBookingPlatform.Application.Queries;
namespace TravelBookingPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class HotelsController : ControllerBase
{
    private readonly IMediator _mediator;

    public HotelsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetHotels()
    {
        var getHotelsAdminQuery = new GetAllHotelsQuery();

        var hotels = await _mediator.Send(getHotelsAdminQuery);
        return Ok(hotels);
    }

    [HttpGet("{hotelId}")]
    public async Task<IActionResult> GetHotel([FromRoute] int hotelId)
    {
        var getHotelAdminQuery = new GetHotelQuery
        {
            HotelId = hotelId
        };

        var hotel = await _mediator.Send(getHotelAdminQuery);
        return Ok(hotel);
    }

    [HttpPut("{hotelId}")]
    public async Task<IActionResult> UpdateHotel([FromRoute] int hotelId, [FromBody] UpdateHotelDto updateHotelDto)
    {
        var updateHotelAdminCommand = new UpdateHotelAdminCommand
        {
            HotelId = hotelId,
            UpdateHotelDto = updateHotelDto
        };

        var hotel = await _mediator.Send(updateHotelAdminCommand);
        return Ok(hotel);
    }

    [HttpDelete("{hotelId}")]
    public async Task<IActionResult> DeleteHotel([FromRoute] int hotelId)
    {
        var deleteHotelAdminCommand = new DeleteHotelAdminCommand
        {
            HotelId = hotelId
        };

        await _mediator.Send(deleteHotelAdminCommand);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchHotels([FromQuery] SearchHotelCriteria searchHotelCriteria)
    {
        var searchHotelQuery = new SearchHotelsQuery
        {
            SearchHotel = searchHotelCriteria
        };

        var hotels = await _mediator.Send(searchHotelQuery);
        return Ok(hotels);
    }

    [HttpPost("{hotelId}/rooms")]
    public async Task<IActionResult> CreateRoomForHotel([FromBody] CreateRoomDto createRoomDto, [FromRoute] int hotelId)
    {
        var createRoomAdminCommand = new CreateRoomCommand
        {
            CreateRoomDto = createRoomDto,
            HotelId = hotelId
        };

        var room = await _mediator.Send(createRoomAdminCommand);
        return Ok(room);
    }

    [HttpGet("{hotelId}/rooms")]
    public async Task<IActionResult> GetRoomsForHotel([FromRoute] int hotelId)
    {
        var getRoomsForHotelQuery = new GetAllRoomsForHotelQuery
        {
            HotelId = hotelId
        };

        var rooms = await _mediator.Send(getRoomsForHotelQuery);
        return Ok(rooms);
    }

    [HttpGet("{hotelId}/available-rooms")]
    public async Task<IActionResult> GetAvailableRoomsForHotel([FromRoute] int hotelId)
    {
        var getAvailableRoomsForHotelQuery = new GetAvailableRoomsForHotelQuery
        {
            HotelId = hotelId
        };

        var rooms = await _mediator.Send(getAvailableRoomsForHotelQuery);
        return Ok(rooms);
    }

    [HttpGet("{hotelId}/images")]
    public async Task<IActionResult> GetHotelImages([FromRoute] int hotelId)
    {
        var getImagesForHotelQuery = new GetImagesForHotelQuery
        {
            HotelId = hotelId
        };

        var images = await _mediator.Send(getImagesForHotelQuery);
        return Ok(images);
    }

    [HttpPost("{hotelId}/images")]
    public async Task<IActionResult> CreateImageForHotel([FromRoute] int hotelId, [FromBody] CreateImageDto createImageDto)
    {
        var createImageForHotelCommand = new CreateImageForHotelCommand
        {
            HotelId = hotelId,
            CreateImageDto = createImageDto
        };

        var image = await _mediator.Send(createImageForHotelCommand);
        return Ok(image);
    }
}