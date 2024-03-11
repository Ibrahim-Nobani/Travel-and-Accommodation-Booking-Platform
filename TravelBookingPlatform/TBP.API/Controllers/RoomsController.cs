using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Commands;
using TravelBookingPlatform.Application.Queries;
namespace TravelBookingPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RoomsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetRooms()
    {
        var getRoomsAdminQuery = new GetAllRoomsQuery();

        var rooms = await _mediator.Send(getRoomsAdminQuery);
        return Ok(rooms);
    }

    [HttpGet("{roomId}")]
    public async Task<IActionResult> GetRoom([FromRoute] int roomId)
    {
        var getRoomAdminQuery = new GetRoomQuery
        {
            RoomId = roomId
        };

        var room = await _mediator.Send(getRoomAdminQuery);
        return Ok(room);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomDto createRoomDto)
    {
        var createRoomAdminCommand = new CreateRoomCommand
        {
            CreateRoomDto = createRoomDto
        };

        var room = await _mediator.Send(createRoomAdminCommand);
        return Ok(room);
    }

    [HttpDelete("{roomId}")]
    public async Task<IActionResult> DeleteRoom([FromRoute] int roomId)
    {
        var deleteRoomAdminCommand = new DeleteRoomCommand
        {
            RoomId = roomId
        };

        await _mediator.Send(deleteRoomAdminCommand);
        return NoContent();
    }

    [HttpPut("{roomId}")]
    public async Task<IActionResult> UpdateRoom([FromRoute] int roomId, [FromBody] UpdateRoomDto updateRoomDto)
    {
        var updateRoomAdminCommand = new UpdateRoomCommand
        {
            RoomId = roomId,
            UpdateRoomDto = updateRoomDto
        };

        var room = await _mediator.Send(updateRoomAdminCommand);
        return Ok(room);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchRooms([FromQuery] SearchRoomCriteria searchRoomCriteria)
    {
        var searchRoomsQuery = new SearchRoomsQuery
        {
            SearchRoom = searchRoomCriteria
        };

        var rooms = await _mediator.Send(searchRoomsQuery);
        return Ok(rooms);
    }

    [HttpGet("{roomId}/images")]
    public async Task<IActionResult> GetRoomImages([FromRoute] int roomId)
    {
        var getImagesForRoomQuery = new GetImagesForRoomQuery
        {
            RoomId = roomId
        };

        var images = await _mediator.Send(getImagesForRoomQuery);
        return Ok(images);
    }

    [HttpPost("{roomId}/images")]
    public async Task<IActionResult> CreateImageForRoom([FromRoute] int roomId, [FromBody] CreateImageDto createImageDto)
    {
        var createImageForRoomCommand = new CreateImageForRoomCommand
        {
            RoomId = roomId,
            CreateImageDto = createImageDto
        };

        var image = await _mediator.Send(createImageForRoomCommand);
        return Ok(image);
    }
}