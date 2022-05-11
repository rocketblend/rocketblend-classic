namespace RocketBlend.FileDownloader.Interfaces;

/// <summary>
/// The extractor service interface.
/// </summary>
public interface IFileExtractorService
{
    /// <summary>
    /// Extracts the file.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="installPath">The install path.</param>
    public string ExtractFile(string path, string installPath);
}
