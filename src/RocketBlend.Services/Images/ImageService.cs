using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Images;

namespace RocketBlend.Services.Images;

/// <summary>
/// The image service.
/// </summary>
public class ImageService : IImageService
{
    /// <summary>
    /// The picsum url.
    /// </summary>
    private const string PicsumUrl = "https://picsum.photos/";

    /// <summary>
    /// The thumbnail directory.
    /// </summary>
    private const string ThumbnailDirectory = "thumbnails";

    private readonly IImageProcessingService _imageProcessingService;
    private readonly IDirectoryService _directoryService;
    private readonly IFileService _fileService;
    private readonly IFileNameGenerationService _fileNameGenerationService;
    private readonly IPathService _pathService;

    private readonly Random _rnd = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageService"/> class.
    /// </summary>
    /// <param name="imageProcessingService">The image processing service.</param>
    /// <param name="directoryService">The directory service.</param>
    /// <param name="fileService">The file service.</param>
    /// <param name="fileNameGenerationService">The file name generation service.</param>
    /// <param name="pathService">The path service.</param>
    public ImageService(
        IImageProcessingService imageProcessingService,
        IDirectoryService directoryService,
        IFileService fileService,
        IFileNameGenerationService fileNameGenerationService,
        IPathService pathService)
    {
        this._imageProcessingService = imageProcessingService;
        this._directoryService = directoryService;
        this._fileService = fileService;
        this._fileNameGenerationService = fileNameGenerationService;
        this._pathService = pathService;
    }

    /// <inheritdoc />
    public string CreateThumbnail(string sourceFile, int width, int height)
    {
        // Ensure thumbnail directory is created.
        this._directoryService.Create(ThumbnailDirectory);

        var initialName = this._pathService.GetFileNameWithoutExtension(sourceFile);
        var generatedName = this._fileNameGenerationService.GenerateName($"{initialName}-{width}-{height}.jpg", ThumbnailDirectory);
        var outputFile = this._pathService.Combine(ThumbnailDirectory, generatedName);

        this._imageProcessingService.CreateThumbnail(sourceFile, outputFile, width, height);

        return this._pathService.GetRelativePath("//", outputFile);
    }

    /// <inheritdoc />
    public string GetRandomImageUrl(int width, int height)
    {
        return $"{PicsumUrl}/id/{this._rnd.Next(1, 200)}/{width}/{height}";
    }
}