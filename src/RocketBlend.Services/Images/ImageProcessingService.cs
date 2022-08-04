using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Images;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace RocketBlend.Services.Images;

/// <summary>
/// The image processing service.
/// </summary>
public class ImageProcessingService : IImageProcessingService
{
    private readonly IFileService _fileService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageProcessingService"/> class.
    /// </summary>
    /// <param name="directoryService">The directory service.</param>
    public ImageProcessingService(IFileService fileService)
    {
        this._fileService = fileService;
    }

    /// <inheritdoc />
    public void CreateThumbnail(string sourceFile, string destinationFile, int width, int height)
    {
        if (!this._fileService.CheckIfExists(sourceFile))
        {
            return;
        }

        using var image = Image.Load(sourceFile);
        image.Mutate(i => i.Resize(new ResizeOptions
        {
            Size = new Size(width, height),
            Mode = ResizeMode.Crop
        }));

        image.SaveAsJpeg(destinationFile);
    }
}
