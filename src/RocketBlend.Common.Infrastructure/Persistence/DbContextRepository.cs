using Microsoft.EntityFrameworkCore;
using RocketBlend.Common.CrossCuttingConcerns.OS;
using RocketBlend.Common.Domain.Entities;
using RocketBlend.Common.Domain.Repositories;

namespace RocketBlend.Infrastructure.Persistence;

/// <summary>
/// The db context repository.
/// </summary>
public class DbContextRepository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
    where TEntity : AggregateRoot<TKey>
    where TDbContext : DbContext, IUnitOfWork
{
    private readonly TDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    /// <summary>
    /// Gets the db set.
    /// </summary>
    protected DbSet<TEntity> DbSet => this._dbContext.Set<TEntity>();

    /// <summary>
    /// Gets the unit of work.
    /// </summary>
    public IUnitOfWork UnitOfWork
    {
        get
        {
            return this._dbContext;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DbContextRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The db context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    public DbContextRepository(TDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        this._dbContext = dbContext;
        this._dateTimeProvider = dateTimeProvider;
    }

    /// <inheritdoc />
    public IQueryable<TEntity> GetAll()
    {
        return this._dbContext.Set<TEntity>();
    }

    /// <inheritdoc />
    public Task<TSource?> FirstOrDefaultAsync<TSource>(IQueryable<TSource> query, CancellationToken cancellationToken = default)
    {
        return query.FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<TSource?> SingleOrDefaultAsync<TSource>(IQueryable<TSource> query, CancellationToken cancellationToken = default)
    {
        return query.SingleOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<List<TSource>> ToListAsync<TSource>(IQueryable<TSource> query)
    {
        return query.ToListAsync();
    }

    /// <inheritdoc />
    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.MarkCreated(this._dateTimeProvider.OffsetUtcNow);
        await this.DbSet.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public void Update(TEntity entity)
    {
        entity.MarkModified(this._dateTimeProvider.OffsetUtcNow);
    }

    /// <inheritdoc />
    public void Delete(TEntity entity)
    {
        this.DbSet.Remove(entity);
    }
}