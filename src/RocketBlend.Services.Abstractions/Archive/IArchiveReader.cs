namespace RocketBlend.Services.Abstractions.Archive;

/// <summary>
/// The archive reader.
/// </summary>
public interface IArchiveReader
{
    /// <summary>
    /// Extracts the async.
    /// </summary>
    /// <param name="archivePath">The archive path.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <returns>A Task.</returns>
    Task ExtractAsync(string archivePath, string outputDirectory);
}