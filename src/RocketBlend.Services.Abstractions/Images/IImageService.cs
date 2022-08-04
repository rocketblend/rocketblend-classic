namespace RocketBlend.Services.Abstractions.Images;

/// <summary>
/// The image service.
/// </summary>
public interface IImageService
{
    /// <summary>
    /// Gets the random image url.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <returns>A string.</returns>
    string GetRandomImageUrl(int width, int height);


    /// <summary>
    /// Creates the thumbnail.
    /// </summary>
    /// <param name="sourceFile">The source file.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    string CreateThumbnail(string sourceFile, int width, int height);
}
