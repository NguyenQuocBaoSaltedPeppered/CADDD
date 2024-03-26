using FluentValidation;

namespace rendezvousBistro.Application.Menus.Commands;

/// <summary>
/// Create menu command validator
/// </summary>
public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
{
    /// <summary>
    /// Rule of create menu command validator
    /// </summary>
    public CreateMenuCommandValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
        RuleFor(v => v.Description).NotEmpty();
        RuleFor(v => v.Sections).NotEmpty();
    }
}