namespace RocketBlend.Services.Abstractions.Models;

/// <summary>
/// The blender install with download model.
/// </summary>
public class BlenderInstallWithDownloadModel
{
    /// <summary>
    /// Gets the blender install model.
    /// </summary>
    public BlenderInstallModel Install {get;}

    /// <summary>
    /// Gets the download model.
    /// </summary>
    public DownloadModel? Download { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BlenderInstallWithDownloadModel"/> class.
    /// </summary>
    /// <param name="installModel">The install model.</param>
    /// <param name="downloadModel">The download model.</param>
    public BlenderInstallWithDownloadModel(BlenderInstallModel installModel, DownloadModel? downloadModel)
    {
        this.Install = installModel;
        this.Download = downloadModel;
    }
}
