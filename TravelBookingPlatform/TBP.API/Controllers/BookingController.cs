using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Commands;
using TravelBookingPlatform.Application.Queries;
namespace TravelBookingPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BookingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        var getAllBookingsQuery = new GetAllBookingsQuery();

        var bookings =await  _mediator.Send(getAllBookingsQuery);
        return Ok(bookings);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDto createBookingDto)
    {
        var createBookingAdminCommand = new CreateBookingCommand
        {
            CreateBookingDto = createBookingDto
        };

        var booking = await _mediator.Send(createBookingAdminCommand);
        return Ok(booking);
    }
}
