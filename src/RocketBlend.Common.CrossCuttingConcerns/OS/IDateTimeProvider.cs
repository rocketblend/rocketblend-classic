namespace RocketBlend.Common.CrossCuttingConcerns.OS;

/// <summary>
/// The date time provider.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Gets the now.
    /// </summary>
    DateTime Now { get; }


    /// <summary>
    /// Gets the UTC now.
    /// </summary>
    DateTime UtcNow { get; }

    /// <summary>
    /// Gets the offset now.
    /// </summary>
    DateTimeOffset OffsetNow { get; }

    /// <summary>
    /// Gets the offset UTC now.
    /// </summary>
    DateTimeOffset OffsetUtcNow { get; }

}