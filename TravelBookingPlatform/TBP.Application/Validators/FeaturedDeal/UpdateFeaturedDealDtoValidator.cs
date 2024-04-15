using FluentValidation;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Validators;

public class UpdateFeaturedDealDtoValidator : AbstractValidator<UpdateFeaturedDealDto>
{
    public UpdateFeaturedDealDtoValidator()
    {
        RuleFor(deal => deal.RoomId)
            .NotEmpty().WithMessage("Room ID is required.")
            .GreaterThan(0).WithMessage("Room ID must be greater than 0.");

        RuleFor(deal => deal.OriginalPrice)
            .NotEmpty().WithMessage("Original price is required.")
            .GreaterThan(0).WithMessage("Original price must be greater than 0.");

        RuleFor(deal => deal.DiscountedPrice)
            .NotEmpty().WithMessage("Discounted price is required.")
            .GreaterThan(0).WithMessage("Discounted price must be greater than 0.")
            .LessThan(deal => deal.OriginalPrice).WithMessage("Discounted price must be less than the original price.");
    }
}
