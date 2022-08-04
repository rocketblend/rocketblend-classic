using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Images;
using RocketBlend.Services.Abstractions.Models.Blender;
using RocketBlend.Services.Abstractions.Models.Projects;
using RocketBlend.Services.Abstractions.Projects;

namespace RocketBlend.Services.Projects;

/// <summary>
/// The project factory.
/// </summary>
public class ProjectFactory : IProjectFactory
{
    private readonly IColorService _colorService;
    private readonly IImageService _imageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectFactory"/> class.
    /// </summary>
    /// <param name="colorService">The color service.</param>
    public ProjectFactory(
        IColorService colorService,
        IImageService imageService)
    {
        this._colorService = colorService;
        this._imageService = imageService;
    }

    /// <inheritdoc />
    public ProjectModel Create(string projectName, IList<BlendFileModel> blendFiles)
    {
        //var imagePath = Path.Combine("D:/Pictures/Backgrounds/pawel-czerwinski-iuhLxpgSAuA-unsplash.jpg");

        var project = new ProjectModel()
        {
            Name = projectName,
            //BackgroundColor = this._colorService.RandomColor(),
            //BackgroundImagePath = this._imageService.CreateThumbnail(imagePath, 400, 300),
            BackgroundImagePath = this._imageService.GetRandomImageUrl(300, 200)
        };

        project.BlendFiles.AddRange(blendFiles);
        return project;
    }
}