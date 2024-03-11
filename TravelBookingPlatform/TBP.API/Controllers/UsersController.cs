using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Commands;
using TravelBookingPlatform.Application.Queries;
namespace TravelBookingPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById([FromRoute] int userId)
    {
        var getUserQuery = new GetUserQuery
        {
            UserId = userId
        };

        var user = await _mediator.Send(getUserQuery);
        return Ok(user);
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int userId)
    {
        var deleteUserCommand = new DeleteUserCommand
        {
            UserId = userId
        };

        await _mediator.Send(deleteUserCommand);
        return NoContent();
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UpdateUserDto updateUserDto)
    {
        var updateUserCommand = new UpdateUserCommand
        {
            UserId = userId,
            UpdateUserDto = updateUserDto
        };

        var user = await _mediator.Send(updateUserCommand);
        return Ok(user);
    }

    [HttpGet("{userId}/recent-hotels")]
    public async Task<IActionResult> GetUserRecentlyVisitedHotels([FromRoute] int userId, [FromQuery] PaginationParameters paginationParameters)
    {
        var getUserRecentlyVisitedHotelsQuery = new GetPaginatedUserRecentlyVisitedHotelsQuery
        {
            UserId = userId,
            PaginationParameters = paginationParameters
        };

        var recentlyVisitedHotels = await _mediator.Send(getUserRecentlyVisitedHotelsQuery);
        return Ok(recentlyVisitedHotels);
    }
}