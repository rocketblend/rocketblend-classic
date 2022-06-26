using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Archive;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace RocketBlend.Services.Archives;

/// <summary>
/// The archive reader.
/// </summary>
public class ArchiveReader : IArchiveReader
{
    private readonly IFileService _fileService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchiveReader"/> class.
    /// </summary>
    /// <param name="fileService">The file service.</param>
    public ArchiveReader(
        IFileService fileService)
    {
        this._fileService = fileService;
    }

    /// <inheritdoc />
    public async Task ExtractAsync(string archivePath, string outputDirectory)
    {
        await using var inStream = this._fileService.OpenRead(archivePath);
        using var reader = ReaderFactory.Open(inStream);

        var options = new ExtractionOptions
        {
            ExtractFullPath = true,
            Overwrite = true
        };

        reader.WriteAllToDirectory(outputDirectory, options);
    }
}