using FluentValidation;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Validators;

public class UpdateCityDtoValidator : AbstractValidator<UpdateCityDto>
{
    public UpdateCityDtoValidator()
    {
        RuleFor(city => city.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(city => city.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100).WithMessage("Country cannot exceed 50 characters.");

        RuleFor(city => city.PostOffice)
            .NotEmpty().WithMessage("PostOffice is required.")
            .MaximumLength(100).WithMessage("PostOffice cannot exceed 100 characters.");

        RuleFor(city => city.ThumbnailImageUrl)
            .NotEmpty().WithMessage("Thumbnail image URL is required.")
            .MaximumLength(255).WithMessage("Thumbnail image URL cannot exceed 255 characters.")
            .Must(url => Uri.TryCreate(url, UriKind.Relative, out _))
            .WithMessage("Thumbnail image URL must be a valid URL.");
    }
}
