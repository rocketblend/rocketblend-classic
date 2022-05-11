using RocketBlend.Common.Domain.Entities;

namespace RocketBlend.Common.Application.Interfaces;

/// <summary>
/// The cud service.
/// </summary>
public interface ICrudService<TEntity, TKey>
    where TEntity : AggregateRoot<TKey>
{
    /// <summary>
    /// Gets the by id async.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>A Task.</returns>
    Task<TEntity?> GetByIdAsync(TKey id, bool throwIfNotFound = false);

    /// <summary>
    /// Creates the async.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>A Task.</returns>
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the async.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>A Task.</returns>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes the async.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns>A Task.</returns>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
