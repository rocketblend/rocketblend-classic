using RocketBlend.Common.Domain.Repositories;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Domain.Repositories;

/// <summary>
/// The project query options.
/// </summary>
public class ProjectQueryOptions
{
    /// <summary>
    /// Gets or sets the project name.
    /// </summary>
    public string? ProjectName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether include install.
    /// </summary>
    public bool IncludeInstall { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether as no tracking.
    /// </summary>
    public bool AsNoTracking { get; set; }
}

/// <summary>
/// The project repository.
/// </summary>
public interface IProjectRepository : IRepository<Project, Guid>
{
    /// <inheritdoc />
    IQueryable<Project> Get(ProjectQueryOptions queryOptions);
}
