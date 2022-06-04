namespace RocketBlend.Services.Abstractions.Models.EventArgs;

/// <summary>
/// The blender installs list changed event args.
/// </summary>
public class BlenderInstallsListChangedEventArgs : System.EventArgs
{
    /// <summary>
    /// Gets the model.
    /// </summary>
    public BlenderInstallModel Model { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlenderInstallsListChangedEventArgs"/> class.
    /// </summary>
    /// <param name="model">The model.</param>
    public BlenderInstallsListChangedEventArgs(BlenderInstallModel model)
    {
        this.Model = model;
    }
}
