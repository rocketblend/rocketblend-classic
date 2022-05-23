using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Application.Queries.Builds;
using RocketBlend.Common.Application.Mappings;

namespace RocketBlend.Core.Models;

/// <summary>
/// The build model.
/// </summary>
public class BuildModel : ReactiveObject, IMapFrom<BuildDto>
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    [Reactive] public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [Reactive] public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the tag.
    /// </summary>
    [Reactive] public string Tag { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the hash.
    /// </summary>
    [Reactive] public string Hash { get; set; } = string.Empty;
}
