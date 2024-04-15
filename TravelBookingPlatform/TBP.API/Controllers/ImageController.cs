using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Commands;
using TravelBookingPlatform.Application.Queries;
namespace TravelBookingPlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ImagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{imageId}")]
    public async Task<IActionResult> GetImage([FromRoute] int imageId)
    {
        var getImageQuery = new GetImageQuery
        {
            ImageId = imageId
        };

        var image = await _mediator.Send(getImageQuery);
        return Ok(image);
    }

    [HttpDelete("{imageId}")]
    public async Task<IActionResult> DeleteImage([FromRoute] int imageId)
    {
        var deleteImageCommand = new DeleteImageCommand
        {
            ImageId = imageId
        };

        await _mediator.Send(deleteImageCommand);
        return NoContent();
    }

    [HttpPut("{imageId}")]
    public async Task<IActionResult> UpdateImage([FromRoute] int imageId, [FromBody] UpdateImageDto updateImageDto)
    {
        var updateImageCommand = new UpdateImageCommand
        {
            ImageId = imageId,
            UpdateImageDto = updateImageDto
        };

        var image = await _mediator.Send(updateImageCommand);
        return Ok(image);
    }
}