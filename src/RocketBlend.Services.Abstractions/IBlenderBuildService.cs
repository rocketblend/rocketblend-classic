using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Services.Abstractions;

/// <summary>
/// The blender build service.
/// </summary>
public interface IBlenderBuildService
{
    /// <summary>
    /// Gets the blender build models.
    /// </summary>
    IReadOnlyCollection<BlenderBuildModel> BlenderBuilds { get; }

    /// <summary>
    /// Initializes the.
    /// </summary>
    public Task Initialize();

    /// <summary>
    /// Refreshes the.
    /// </summary>
    public Task Refresh();
}
