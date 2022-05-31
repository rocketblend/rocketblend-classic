using RocketBlend.Services.Environment.Interfaces;

namespace RocketBlend.Services.Environment.Implementations;

/// <summary>
/// The date time provider.
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime Now => DateTime.Now;
}