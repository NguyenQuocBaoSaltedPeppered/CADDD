using rendezvousBistro.Domain.Entities;

namespace rendezvousBistro.Application.Services.Authentication;

public record AuthenticationResult (
    User User,
    string Token
);