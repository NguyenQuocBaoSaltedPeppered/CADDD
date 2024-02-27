using ErrorOr;
using MediatR;
using rendezvousBistro.Application.Authentication.Common;

namespace rendezvousBistro.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<ErrorOr<AuthenticationResult>>;