using rendezvousBistro.Application.Common.Interfaces.Persistence;
using rendezvousBistro.Domain.Entities;

namespace rendezvousBistro.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = [];
    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(user => user.Email == email);
    }
}