using RocketBlend.Application.Queries.Installs;
using RocketBlend.Common.Application.Mappings;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Application.Queries.Projects;

public record ProjectDto(
    Guid Id,
    string Name,
    string FileName,
    string FilePath,
    bool IsValid,
    InstallDto Install,
    DateTimeOffset LastLaunched,
    DateTimeOffset CreatedDateTime,
    DateTimeOffset? UpdatedDateTime) : IMapFrom<Project>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectDto"/> class.
    /// Needed for auto mapper.
    /// </summary>
    public ProjectDto() : this(
        default,
        string.Empty,
        string.Empty,
        string.Empty,
        default,
        new(),
        default,
        default,
        default
    ) { }
}