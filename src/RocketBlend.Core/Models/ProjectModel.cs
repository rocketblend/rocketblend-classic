using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Application.Queries.Projects;
using RocketBlend.Common.Application.Mappings;

namespace RocketBlend.Core.Models;

/// <summary>
/// The project model.
/// </summary>
public class ProjectModel : ReactiveObject, IMapFrom<ProjectDto>
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
    /// Gets or sets the file name.
    /// </summary>
    [Reactive] public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the file location.
    /// </summary>
    [Reactive] public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the version.
    /// </summary>
    [Reactive] public InstallModel Install { get; set; } = new();

    /// <summary>
    /// Gets or sets a value indicating whether is valid.
    /// </summary>
    [Reactive] public bool IsValid { get; set; } = false;

    /// <summary>
    /// Gets or sets the last launched.
    /// </summary>
    [Reactive] public DateTimeOffset? LastLaunched { get; set; }
}
