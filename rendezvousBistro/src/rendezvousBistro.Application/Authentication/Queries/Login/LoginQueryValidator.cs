using FluentValidation;

namespace rendezvousBistro.Application.Authentication.Queries.Login;

/// <summary>
/// Login query validator
/// </summary> <summary>
/// 
/// </summary>
public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    /// <summary>
    /// Rule of login query validator
    /// </summary>
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}