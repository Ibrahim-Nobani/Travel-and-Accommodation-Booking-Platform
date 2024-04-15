using MediatR;
using Microsoft.AspNetCore.Mvc;
using TravelBookingPlatform.Domain.Entities;
using TravelBookingPlatform.Application.DTOs;
using TravelBookingPlatform.Application.Commands;
using TravelBookingPlatform.Application.Queries;
namespace TravelBookingPlatform.API.Controllers;

[ApiController]
[Route("api/featured-deals")]
public class FeaturedDealsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FeaturedDealsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFeaturedDeals([FromQuery] PaginationParameters paginationParameters)
    {
        var getFeaturedDealsQuery = new GetPaginatedFeaturedDealsQuery
        {
            PaginationParameters = paginationParameters
        };

        var featuredDeals = await _mediator.Send(getFeaturedDealsQuery);

        return Ok(featuredDeals);
    }

    [HttpGet("{featuredDealId}")]
    public async Task<IActionResult> GetFeaturedDeal([FromRoute] int featuredDealId)
    {
        var getFeaturedDealQuery = new GetFeaturedDealQuery
        {
            FeaturedDealId = featuredDealId
        };

        var featuredDeal = await _mediator.Send(getFeaturedDealQuery);

        return Ok(featuredDeal);
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeaturedDeal([FromBody] CreateFeaturedDealDto createFeaturedDealDto)
    {
        var createFeaturedDealAdminCommand = new CreateFeaturedDealCommand
        {
            CreateFeaturedDealDto = createFeaturedDealDto
        };

        var featuredDeal = await _mediator.Send(createFeaturedDealAdminCommand);
        return Ok(featuredDeal);
    }

    [HttpPut("{featuredDealId}")]
    public async Task<IActionResult> UpdateFeaturedDeal([FromRoute] int featuredDealId, [FromBody] UpdateFeaturedDealDto updateFeaturedDealDto)
    {
        var updateFeaturedDealAdminCommand = new UpdateFeaturedDealCommand
        {
            FeaturedDealId = featuredDealId,
            UpdateFeaturedDealDto = updateFeaturedDealDto
        };

        var featuredDeal = await _mediator.Send(updateFeaturedDealAdminCommand);
        return Ok(featuredDeal);
    }

    [HttpDelete("{featuredDealId}")]
    public async Task<IActionResult> DeleteFeaturedDeal([FromRoute] int featuredDealId)
    {
        var deleteFeaturedDealCommand = new DeleteFeaturedDealCommand
        {
            FeaturedDealId = featuredDealId
        };

        await _mediator.Send(deleteFeaturedDealCommand);
        return NoContent();
    }
}
