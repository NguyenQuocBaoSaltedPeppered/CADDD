using ErrorOr;
using MediatR;
using rendezvousBistro.Application.Authentication.Common;

namespace rendezvousBistro.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}