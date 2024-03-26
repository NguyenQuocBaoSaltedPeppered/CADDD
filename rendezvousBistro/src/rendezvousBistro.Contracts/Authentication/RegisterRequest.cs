namespace rendezvousBistro.Contracts.Authentication;

/// <summary>
/// Register request
/// </summary>
/// <param name="FirstName"></param>
/// <param name="LastName"></param>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);