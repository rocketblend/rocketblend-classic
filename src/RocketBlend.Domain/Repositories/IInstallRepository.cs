using RocketBlend.Common.Domain.Repositories;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Domain.Repositories;

/// <summary>
/// The install query options.
/// </summary>
public class InstallQueryOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether as no tracking.
    /// </summary>
    public bool AsNoTracking { get; set; }
}

/// <summary>
/// The install repository.
/// </summary>
public interface IInstallRepository : IRepository<Install, Guid>
{
    /// <inheritdoc />
    IQueryable<Install> Get(InstallQueryOptions queryOptions);
}
