using rendezvousBistro.Application.Common.Interfaces.Services;

namespace rendezvousBistro.Infrastructure.Services;

/// <inheritdoc cref="IDateTimeProvider"/>
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}