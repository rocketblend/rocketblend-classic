using System.Runtime.Serialization;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RocketBlend.Services.Abstractions.Models.Blender;

namespace RocketBlend.Services.Abstractions.Models.Projects;

/// <summary>
/// The project model.
/// </summary>
[DataContract]
public class ProjectModel : ReactiveObject, IHasKey<Guid>
{
    /// <inheritdoc />
    [DataMember]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the install id.
    /// </summary>
    [Reactive]
    [DataMember]
    public Guid InstallId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [Reactive]
    [DataMember]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the launch args.
    /// </summary>
    [Reactive]
    [DataMember]
    public string LaunchArgs { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the background color.
    /// </summary>
    [Reactive]
    [DataMember]
    public string BackgroundColor { get; set; } = "#414141";

    /// <summary>
    /// Gets or sets the packages.
    /// </summary>
    [DataMember]
    public ObservableCollectionExtended<string> Packages { get; set; } = new();

    /// <summary>
    /// Gets or sets the blend files.
    /// </summary>
    [DataMember]
    public ObservableCollectionExtended<BlendFileModel> BlendFiles { get; set; } = new();

    /// <summary>
    /// Gets or sets the tags.
    /// </summary>
    [DataMember]
    public ObservableCollectionExtended<string> Tags { get; set; } = new();

    // Add state
}
