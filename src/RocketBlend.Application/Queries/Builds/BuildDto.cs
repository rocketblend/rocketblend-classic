using RocketBlend.Common.Application.Mappings;
using RocketBlend.Domain.Entities;

namespace RocketBlend.Application.Queries.Builds;

public record BuildDto(Guid Id, string Name, string Tag, string Hash, string DownloadUrl, string FileSize, DateTimeOffset CreatedDateTime, DateTimeOffset? UpdatedDateTime) : IMapFrom<Build>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BuildDto"/> class.
    /// Needed for auto mapper.
    /// </summary>
    public BuildDto() : this(default, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, default, default) { }
}