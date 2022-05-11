namespace RocketBlend.Common.Domain.Entities;

/// <summary>
/// Auditable object interface.
/// </summary>
internal interface IAuditable
{
    /// <summary>
    /// Gets or sets the created date time.
    /// </summary>
    DateTimeOffset? CreatedDateTime { get; }

    /// <summary>
    /// Gets or sets the updated date time.
    /// </summary>
    DateTimeOffset? UpdatedDateTime { get; }

    /// <summary>
    /// Marks the object created.
    /// </summary>
    public void MarkCreated(DateTimeOffset timeOffset);

    /// <summary>
    /// Marks the object modified.
    /// </summary>
    public void MarkModified(DateTimeOffset timeOffset);
}
