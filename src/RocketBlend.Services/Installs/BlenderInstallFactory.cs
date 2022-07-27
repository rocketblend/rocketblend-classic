using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Installs;
using RocketBlend.Services.Abstractions.Models.Blender;
using RocketBlend.Services.Abstractions.Models.Installs;

namespace RocketBlend.Services.Installs;

/// <summary>
/// The blender install factory.
/// </summary>
public class BlenderInstallFactory : IBlenderInstallFactory
{
    private readonly IColorService _colorService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlenderInstallFactory"/> class.
    /// </summary>
    public BlenderInstallFactory(IColorService colorService)
    {
        this._colorService = colorService;
    }

    /// <inheritdoc />
    public BlenderInstallModel Create(string name, string tag, string hash, string path, string downloadUrl)
    {
        // Move
        var executable = new BlenderExecutableModel()
        {
            BuildHash = hash,
            BuildTag = tag,
            SourceUri = new Uri(downloadUrl),
            FullPath = Path.Combine(path, Path.GetFileNameWithoutExtension(downloadUrl), "blender.exe"),
        };

        return new BlenderInstallModel(name, executable, this._colorService.RandomColor());
    }
}
