namespace RocketBlend.Services.Environment.Interfaces;

/// <summary>
/// The date time provider.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Gets the now.
    /// </summary>
    DateTime Now { get; }
}