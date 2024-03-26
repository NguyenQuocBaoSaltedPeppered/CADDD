using rendezvousBistro.Domain.Entities;

namespace rendezvousBistro.Application.Common.Interfaces.Persistence;

/// <summary>
/// User repository interface
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Get user by email
    /// </summary>
    /// <param name="email">Email</param>
    /// <returns>User</returns>
    User? GetUserByEmail(string email);

    /// <summary>
    /// Add new user
    /// </summary>
    /// <param name="user">User</param>
    void AddUser(User user);
}