using RocketBlend.Common.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Reflection;
using RocketBlend.Common.Domain.Entities;
using RocketBlend.Common.Domain.Events;
using RocketBlend.Common.Application.Interfaces;

namespace RocketBlend.Infrastructure.Persistence;

/// <summary>
/// The db context unit of work.
/// </summary>
public class DbContextUnitOfWork<TDbContext> : DbContext, IUnitOfWork
    where TDbContext : DbContext
{
    private IDbContextTransaction? _dbContextTransaction;
    private readonly IDomainEventService _domainEventService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DbContextUnitOfWork"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public DbContextUnitOfWork(DbContextOptions<TDbContext> options, IDomainEventService domainEventService)
        : base(options)
    {
        this._domainEventService = domainEventService;
    }

    /// <inheritdoc />
    public IDisposable BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        this._dbContextTransaction = this.Database.BeginTransaction(isolationLevel);
        return this._dbContextTransaction;
    }

    /// <inheritdoc />
    public async Task<IDisposable> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
    {
        this._dbContextTransaction = await this.Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        return this._dbContextTransaction;
    }

    /// <inheritdoc />
    public void CommitTransaction()
    {
        this._dbContextTransaction?.Commit();
    }

    /// <inheritdoc />
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (this._dbContextTransaction != null)
        {
            await this._dbContextTransaction.CommitAsync(cancellationToken);
        }
    }

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Gets the events to dispatch
        IList<DomainEvent> eventsToDispatch = this.GetDomainEvents().ToList();

        // Do the persisting of work
        var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // Dispatch the previously captured events after persisting was successful.
        await this.DispatchEventsAsync(eventsToDispatch).ConfigureAwait(false);

        return result;
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <summary>
    /// Gets the domain events.
    /// </summary>
    /// <returns>A list of DomainEvents.</returns>
    private IEnumerable<DomainEvent> GetDomainEvents()
    {
        return this.ChangeTracker
            .Entries<IHasDomainEvent>()
            .Select(x => x.Entity.DomainEvents)
            .SelectMany(x => x)
            .Where(domainEvent => !domainEvent.IsPublished);
    }

    /// <summary>
    /// Dispatches the events async.
    /// </summary>
    /// <param name="eventsToDispatch">The events to dispatch.</param>
    /// <returns>A Task.</returns>
    private async Task DispatchEventsAsync(IList<DomainEvent> eventsToDispatch)
    {
        while (eventsToDispatch.Any())
        {
            var domainEventEntity = eventsToDispatch.First();
            domainEventEntity.IsPublished = true;
            await this._domainEventService.Publish(domainEventEntity);
            eventsToDispatch.RemoveAt(0);
        }
    }
}
