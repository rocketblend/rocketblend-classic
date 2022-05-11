using RocketBlend.Common.Domain.Entities;

namespace RocketBlend.Common.Domain.Repositories;

/// <summary>
/// Generic repository interface.
/// </summary>
public interface IRepository<TEntity, TKey>
    where TEntity : AggregateRoot<TKey>
{
    /// <summary>
    /// Gets the unit of work.
    /// </summary>
    IUnitOfWork UnitOfWork { get; }

    /// <summary>
    /// Gets the all.
    /// </summary>
    /// <returns>An IQueryable.</returns>
    IQueryable<TEntity> GetAll();

    /// <summary>
    /// Firsts the or default async.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>A Task.</returns>
    Task<TSource?> FirstOrDefaultAsync<TSource>(IQueryable<TSource> query, CancellationToken cancellationToken = default);

    /// <summary>
    /// Singles the or default async.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>A Task.</returns>
    Task<TSource?> SingleOrDefaultAsync<TSource>(IQueryable<TSource> query, CancellationToken cancellationToken = default);

    /// <summary>
    /// To list async.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>A Task.</returns>
    Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> query);

    /// <summary>
    /// Adds the or update async.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default);


    /// <summary>
    /// Updates the async.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    void Update(TEntity entity);

    /// <summary>
    /// Deletes the.
    /// </summary>
    /// <param name="entity">The entity.</param>
    void Delete(TEntity entity);
}

