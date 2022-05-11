using RocketBlend.Common.Domain.Repositories;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Domain.Repositories;

/// <summary>
/// The build query options.
/// </summary>
public class BuildQueryOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether as no tracking.
    /// </summary>
    public bool AsNoTracking { get; set; }
}

/// <summary>
/// The build repository.
/// </summary>
public interface IBuildRepository : IRepository<Build, Guid>
{
    /// <inheritdoc />
    IQueryable<Build> Get(BuildQueryOptions queryOptions);
}
