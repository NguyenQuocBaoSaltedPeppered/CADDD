using rendezvousBistro.Domain.Entities;

namespace rendezvousBistro.Application.Authentication.Common;

/// <summary>
/// Authentication result
/// </summary>
/// <param name="User"></param>
/// <param name="Token"></param>
public record AuthenticationResult (
    User User,
    string Token
);