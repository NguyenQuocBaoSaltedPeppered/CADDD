using FluentValidation;

namespace rendezvousBistro.Application.Menus.Commands;

public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
{
    public CreateMenuCommandValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
        RuleFor(v => v.Description).NotEmpty();
        RuleFor(v => v.Sections).NotEmpty();
    }
}