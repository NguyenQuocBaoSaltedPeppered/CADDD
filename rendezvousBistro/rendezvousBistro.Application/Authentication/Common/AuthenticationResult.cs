using rendezvousBistro.Domain.Entities;

namespace rendezvousBistro.Application.Authentication.Common;

public record AuthenticationResult (
    User User,
    string Token
);