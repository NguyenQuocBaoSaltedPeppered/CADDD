using rendezvousBistro.Application.Common.Interfaces.Services;

namespace rendezvousBistro.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}