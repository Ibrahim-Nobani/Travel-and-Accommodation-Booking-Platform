using FluentValidation;
using TravelBookingPlatform.Application.DTOs;

public class UpdateRoomDtoValidator : AbstractValidator<UpdateRoomDto>
{
    public UpdateRoomDtoValidator()
    {
        RuleFor(room => room.Number)
            .NotEmpty().WithMessage("Room number is required.")
            .GreaterThan(0).WithMessage("Room number must be greater than 0.");

        RuleFor(room => room.Price)
            .NotEmpty().WithMessage("Price is required.")
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(room => room.ThumbnailImageUrl)
            .NotEmpty().WithMessage("Thumbnail image URL is required.")
            .MaximumLength(255).WithMessage("Thumbnail image URL cannot exceed 255 characters.")
            .Must(url => Uri.TryCreate(url, UriKind.Relative, out _))
            .WithMessage("Thumbnail image URL must be a valid relative URL.");

        RuleFor(room => room.AdultCapacity)
            .NotEmpty().WithMessage("Adult capacity is required.")
            .GreaterThan(0).WithMessage("Adult capacity must be greater than 0.");

        RuleFor(room => room.ChildCapacity)
            .NotEmpty().WithMessage("Child capacity is required.")
            .GreaterThanOrEqualTo(0).WithMessage("Child capacity must be greater than or equal to 0.");

        RuleFor(room => room.Availability)
            .NotNull().WithMessage("Availability is required.");
    }
}
