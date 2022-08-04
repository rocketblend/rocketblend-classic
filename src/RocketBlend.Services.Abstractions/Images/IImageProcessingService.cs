namespace RocketBlend.Services.Abstractions.Images;

/// <summary>
/// The image service.
/// </summary>
public interface IImageProcessingService
{
    /// <summary>
    /// Creates the thumbnail.
    /// </summary>
    /// <param name="sourceFile">The source file.</param>
    /// <param name="destinationFile">The destination file.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    void CreateThumbnail(string sourceFile, string destinationFile, int width, int height);
}
