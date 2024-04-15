using FluentValidation;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Validators;

public class UpdateHotelDtoValidator : AbstractValidator<UpdateHotelDto>
{
    public UpdateHotelDtoValidator()
    {
        RuleFor(hotel => hotel.Name)
            .NotEmpty().WithMessage("Hotel name is required.")
            .MaximumLength(100).WithMessage("Hotel name cannot exceed 100 characters.");

        RuleFor(hotel => hotel.StarRating)
            .InclusiveBetween(1, 5).When(hotel => hotel.StarRating.HasValue)
            .WithMessage("Star rating must be between 1 and 5.");

        RuleFor(hotel => hotel.Location)
            .NotEmpty().WithMessage("Location is required.")
            .MaximumLength(100).WithMessage("Location cannot exceed 150 characters.");

        RuleFor(hotel => hotel.Owner)
            .NotEmpty().WithMessage("Owner name is required.")
            .MaximumLength(100).WithMessage("Owner name cannot exceed 100 characters.");

        RuleFor(hotel => hotel.ThumbnailImageUrl)
            .NotEmpty().WithMessage("Thumbnail image URL is required.")
            .MaximumLength(255).WithMessage("Thumbnail image URL cannot exceed 255 characters.")
            .Must(url => Uri.TryCreate(url, UriKind.Relative, out _))
            .WithMessage("Thumbnail image URL must be a valid absolute URL.");
    }
}
