using DynamicData;
using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Services.Abstractions.Builds;

/// <summary>
/// The blender build service.
/// </summary>
public interface IBlenderBuildService
{
    /// <summary>
    /// Connects the.
    /// </summary>
    /// <returns>An IObservable.</returns>
    IObservable<IChangeSet<BlenderBuildModel, Guid>> Connect();

    /// <summary>
    /// Refreshes the.
    /// </summary>
    Task Refresh();
}