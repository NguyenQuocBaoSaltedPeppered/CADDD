using FluentValidation;

namespace rendezvousBistro.Application.Authentication.Commands.Register;

/// <summary>
/// Register command validator
/// </summary>
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    /// <summary>
    /// Rule of register command validator
    /// </summary>
    public RegisterCommandValidator()
    {
        RuleFor(v => v.FirstName)
            .NotEmpty();
            // .WithMessage("First Name is required");

        RuleFor(v => v.LastName)
            .NotEmpty();
            // .WithMessage("Last Name is required");

        RuleFor(v => v.Email)
            .NotEmpty()
            // .WithMessage("Email is required")
            .EmailAddress();
            // .WithMessage("A valid email is required");

        RuleFor(v => v.Password)
            .NotEmpty();
            // .WithMessage("Password is required")
            // .MinimumLength(6)
            // .WithMessage("Password must be at least 6 characters");
    }
}