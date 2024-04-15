using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Commands;
namespace TravelBookingPlatform.API.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAdmin([FromBody] CreateUserAdminDto userDto)
    {
        var createUserAdminCommand = new CreateUserAdminCommand
        {
            CreateUserAdminDto = userDto
        };

        var userResponse = await _mediator.Send(createUserAdminCommand);
        return Ok(userResponse);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUserAdmin([FromRoute] int userId, [FromBody] UpdateUserAdminDto updateUserAdminDto)
    {
        var updateUserAdminCommand = new UpdateUserAdminCommand
        {
            UserId = userId,
            UpdateUserAdminDto = updateUserAdminDto
        };

        var user = await _mediator.Send(updateUserAdminCommand);
        return Ok(user);
    }
}