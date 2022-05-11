using System.IO.Compression;
using RocketBlend.FileDownloader.Interfaces;

namespace RocketBlend.FileDownloader;

/// <summary>
/// The extractor service.
/// </summary>
public class ZipFileExtractorService : IFileExtractorService
{
    /// <inheritdoc />
    public string ExtractFile(string path, string extractPath)
    {
        ZipFile.ExtractToDirectory(path, extractPath);
        File.Delete(path);

        return new DirectoryInfo(extractPath).GetDirectories().OrderByDescending(d => d.LastWriteTimeUtc).First().FullName;
    }
}
