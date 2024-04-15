using FluentValidation;
using TravelBookingPlatform.Application.DTOs;
namespace TravelBookingPlatform.Application.Validators;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator()
    {
        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username is required!")
            .MinimumLength(4).WithMessage("Username must be at least 4 characters!")
            .MaximumLength(30).WithMessage("Username must not exceed 30 characters!")
            .Matches("^[a-zA-Z0-9_-]+$").WithMessage("Username can only contain letters, numbers, underscores, and hyphens.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required!")
            .MinimumLength(7).WithMessage("Password must be at least 7 characters!")
            .MaximumLength(50).WithMessage("Password cannot exceed 50 characters.")
            .Matches(".*[0-9].*").WithMessage("Password must contain at least one digit.");

        RuleFor(user => user.Email)
            .EmailAddress()
            .WithMessage("The email is invalid");
    }
}