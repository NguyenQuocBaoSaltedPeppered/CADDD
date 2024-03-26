using ErrorOr;
using MediatR;
using rendezvousBistro.Application.Authentication.Common;

namespace rendezvousBistro.Application.Authentication.Queries.Login;

/// <summary>
/// Login query
/// </summary>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;