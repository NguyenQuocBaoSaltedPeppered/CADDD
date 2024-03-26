using rendezvousBistro.Domain.Entities;

namespace rendezvousBistro.Application.Common.Interfaces.Authentication;

/// <summary>
/// Jwt token generator interface
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// Generate a token for a user
    /// </summary>
    /// <param name="user">User information</param>
    /// <returns></returns>
    string GenerateToken(User user);
}