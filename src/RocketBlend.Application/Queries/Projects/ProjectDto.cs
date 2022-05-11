using RocketBlend.Application.Queries.Installs;
using RocketBlend.Common.Application.Mappings;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Application.Queries.Projects;

public record ProjectDto(Guid Id, string Name, string Path, InstallDto Install, DateTimeOffset CreatedDateTime, DateTimeOffset? UpdatedDateTime) : IMapFrom<Project>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectDto"/> class.
    /// Needed for auto mapper.
    /// </summary>
    public ProjectDto() : this(default, string.Empty, string.Empty, new(), default, default) { }
}