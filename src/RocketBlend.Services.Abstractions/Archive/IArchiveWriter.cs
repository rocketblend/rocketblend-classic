namespace RocketBlend.Services.Abstractions.Archive;

/// <summary>
/// The archive writer.
/// </summary>
public interface IArchiveWriter
{
    /// <summary>
    /// Packs the async.
    /// </summary>
    /// <param name="files">The files.</param>
    /// <param name="directories">The directories.</param>
    /// <param name="sourceDirectory">The source directory.</param>
    /// <param name="outputFile">The output file.</param>
    /// <returns>A Task.</returns>
    Task PackAsync(IReadOnlyList<string> files, IReadOnlyList<string> directories, string sourceDirectory, string outputFile);
}