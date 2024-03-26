using ErrorOr;
using MediatR;
using rendezvousBistro.Application.Authentication.Common;

namespace rendezvousBistro.Application.Authentication.Commands.Register
{
    /// <summary>
    /// Register command
    /// </summary>
    /// <param name="FirstName"></param>
    /// <param name="LastName"></param>
    /// <param name="Email"></param>
    /// <param name="Password"></param>
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<ErrorOr<AuthenticationResult>>;
}