using RocketBlend.Common.CrossCuttingConcerns.OS;

namespace RocketBlend.Infrastructure.OS;

/// <summary>
/// The date time provider.
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime Now => DateTime.Now;

    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;

    /// <inheritdoc />
    public DateTimeOffset OffsetNow => DateTimeOffset.Now;

    /// <inheritdoc />
    public DateTimeOffset OffsetUtcNow => DateTimeOffset.UtcNow;
}