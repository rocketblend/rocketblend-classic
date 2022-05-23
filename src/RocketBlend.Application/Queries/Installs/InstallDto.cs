using RocketBlend.Application.Queries.Builds;
using RocketBlend.Common.Application.Mappings;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Application.Queries.Installs;

public record InstallDto(Guid Id, BuildDto Build, string FileName, string FileLocation, string LaunchArgs, DateTimeOffset CreatedDateTime, DateTimeOffset? UpdatedDateTime) : IMapFrom<Install>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallDto"/> class.
    /// Needed for auto mapper.
    /// </summary>
    public InstallDto() : this(default, new(), string.Empty, string.Empty, string.Empty, default, default) { }
}