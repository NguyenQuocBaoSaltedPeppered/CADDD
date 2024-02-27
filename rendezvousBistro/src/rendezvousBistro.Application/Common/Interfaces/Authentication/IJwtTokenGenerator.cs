using rendezvousBistro.Domain.Entities;

namespace rendezvousBistro.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}