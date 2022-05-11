
using RocketBlend.Common.Domain.Entities;

namespace RocketBlend.Common.Domain.Events;

/// <summary>
/// Generic entity event.
/// </summary>
public abstract record EntityEvent<TEntity, TKey> : DomainEvent
    where TEntity : Entity<TKey>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EntityEvent"/> class.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public EntityEvent(TEntity entity)
    {
        this.Entity = entity;
    }

    /// <summary>
    /// Gets or sets the entity.
    /// </summary>
    public TEntity Entity { get; set; }
}
