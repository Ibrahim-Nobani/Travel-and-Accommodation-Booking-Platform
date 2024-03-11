using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Commands;
using TravelBookingPlatform.Application.Queries;
namespace TravelBookingPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto userDto)
    {
        var createUserCommand = new CreateUserCommand
        {
            CreateUserDto = userDto
        };

        var userResponse = await _mediator.Send(createUserCommand);
        return Ok(userResponse);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        var loginUserQuery = new LoginUserQuery
        {
            LoginUserDto = loginUserDto
        };

        var userResponse = await _mediator.Send(loginUserQuery);
        return Ok(userResponse);
    }
}