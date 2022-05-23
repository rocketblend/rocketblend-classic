using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Application.Queries.Installs;
using RocketBlend.Common.Application.Mappings;

namespace RocketBlend.Core.Models;

/// <summary>
/// The install model.
/// </summary>
public class InstallModel : ReactiveObject, IMapFrom<InstallDto>
{
    /// <summary>
    /// Gets or sets the id.
    /// </summary>
    [Reactive] public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the build.
    /// </summary>
    [Reactive] public BuildModel Build { get; set; } = new();

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
    [Reactive] public string FileLocation { get; set; } = string.Empty;

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>A bool.</returns>
    public override bool Equals(object? obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            InstallModel model = (InstallModel)obj;
            return (this.Id == model.Id);
        }
    }

    /// <summary>
    /// Gets the hash code.
    /// </summary>
    /// <returns>An int.</returns>
    public override int GetHashCode()
    {
        return this.Id.GetHashCode(); ;
    }
}
