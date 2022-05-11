using RocketBlend.Common.Application.Exceptions;
using RocketBlend.Common.Application.Interfaces;
using RocketBlend.Common.CrossCuttingConcerns.Guards;
using RocketBlend.Common.Domain.Entities;
using RocketBlend.Common.Domain.Repositories;

namespace RocketBlend.Common.Application.Services;

/// <summary>
/// The create read update delete (crud) service.
/// </summary>
public class CrudService<TEntity, TKey> : ICrudService<TEntity, TKey>
    where TEntity : AggregateRoot<TKey>
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IRepository<TEntity, TKey> _repository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CrudService"/> class.
    /// </summary>
    /// <param name="repository">The repository.</param>
    /// <param name="domainEventService">The domain event service.</param>
    public CrudService(IRepository<TEntity, TKey> repository)
    {
        this._unitOfWork = repository.UnitOfWork;
        this._repository = repository;
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetByIdAsync(TKey id, bool throwIfNotFound = false)
    {
        Guard.ArgumentNotNull(id, nameof(TKey));
        var entity = await this._repository.FirstOrDefaultAsync(this._repository.GetAll().Where(x => x.Id!.Equals(id)));
        return entity == null && throwIfNotFound ? throw new NotFoundException(nameof(TEntity), id!) : entity;
    }

    /// <inheritdoc />
    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await this._repository.CreateAsync(entity, cancellationToken);
        await this._unitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        this._repository.Update(entity);
        await this._unitOfWork.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        this._repository.Delete(entity);
        await this._unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

