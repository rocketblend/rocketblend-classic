using RocketBlend.Services.Abstractions.Models.Blender;

namespace RocketBlend.Services.Abstractions.Models.Installs;

/// <summary>
/// The install model.
/// </summary>
public class BlenderInstallModel : IHasKey<Guid>
{
    /// <inheritdoc />
    public Guid Id { get; init; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the background color.
    /// </summary>
    public string BackgroundColor { get; set; }

    // Add state

    /// <summary>
    /// Gets or sets the blender executable.
    /// </summary>
    public BlenderExecutableModel BlenderExecutable { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlenderInstallModel"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="blenderExecutable">The blender executable.</param>
    /// <param name="backgroundColor">The background color.</param>
    public BlenderInstallModel(string name, BlenderExecutableModel blenderExecutable, string backgroundColor = "#414141")
    {
        this.Id = Guid.NewGuid();
        this.Name = name;
        this.BlenderExecutable = blenderExecutable;
        this.BackgroundColor = backgroundColor;
    }
}

