using System.Data;
using RocketBlend.Common.Domain.Events;

namespace RocketBlend.Common.Domain.Repositories;

/// <summary>
/// The unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves the changes.
    /// </summary>
    /// <returns>An int.</returns>
    int SaveChanges();

    /// <summary>
    /// Saves the changes async.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begins the transaction.
    /// </summary>
    /// <param name="isolationLevel">The isolation level.</param>
    /// <returns>An IDisposable.</returns>
    IDisposable BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    /// <summary>
    /// Begins the transaction async.
    /// </summary>
    /// <param name="isolationLevel">The isolation level.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    Task<IDisposable> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the transaction.
    /// </summary>
    void CommitTransaction();

    /// <summary>
    /// Commits the transaction async.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
}