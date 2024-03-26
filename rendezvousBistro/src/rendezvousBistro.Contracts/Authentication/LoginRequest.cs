namespace rendezvousBistro.Contracts.Authentication;

/// <summary>
/// Login request
/// </summary>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record LoginRequest(
    string Email,
    string Password
);