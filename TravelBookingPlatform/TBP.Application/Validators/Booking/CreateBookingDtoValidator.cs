using FluentValidation;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Validators;

public class CreateBookingDtoValidator : AbstractValidator<CreateBookingDto>
{
    public CreateBookingDtoValidator()
    {
        RuleFor(booking => booking.UserId)
            .NotEmpty().WithMessage("User ID is required.")
            .GreaterThan(0).WithMessage("User ID must be greater than 0.");

        RuleFor(booking => booking.RoomId)
            .NotEmpty().WithMessage("Room ID is required.")
            .GreaterThan(0).WithMessage("Room ID must be greater than 0.");

        RuleFor(booking => booking.CheckInDate)
            .NotEmpty().WithMessage("Check-in date is required.")
            .GreaterThan(DateTime.Now).WithMessage("Check-in date must be in the future.");

        RuleFor(booking => booking.CheckOutDate)
            .NotEmpty().WithMessage("Check-out date is required.")
            .GreaterThan(booking => booking.CheckInDate).WithMessage("Check-out date must be after check-in date.");
    }
}
