namespace rendezvousBistro.Application.Common.Interfaces.Services;

/// <summary>
/// DateTime provider interface
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Get the current UTC time
    /// </summary>
    /// <value></value>
    DateTime UtcNow {get; }
}