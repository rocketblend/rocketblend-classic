namespace RocketBlend.Common.Domain.Events;

/// <summary>
/// The domain event.
/// </summary>
public abstract record DomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEvent"/> class.
    /// </summary>
    public DomainEvent()
    {
        this.DateOccurred = DateTime.UtcNow;
    }

    /// <summary>
    /// Gets or sets a value indicating whether is published.
    /// </summary>
    public bool IsPublished { get; set; }

    /// <summary>
    /// Gets or sets the event date time.
    /// </summary>
    public DateTimeOffset DateOccurred { get; set; }
}