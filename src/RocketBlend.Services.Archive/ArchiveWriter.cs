using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Archive;
using SharpCompress.Common;
using SharpCompress.Writers;

namespace RocketBlend.Services.Archives;

/// <summary>
/// The archive writer.
/// </summary>
public class ArchiveWriter : IArchiveWriter
{
    private readonly IFileService _fileService;
    private readonly IPathService _pathService;
    private readonly IDirectoryService _directoryService;
    private readonly ArchiveType _archiveType;
    private readonly WriterOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArchiveWriter"/> class.
    /// </summary>
    /// <param name="fileService">The file service.</param>
    /// <param name="pathService">The path service.</param>
    /// <param name="directoryService">The directory service.</param>
    /// <param name="archiveType">The archive type.</param>
    /// <param name="options">The options.</param>
    public ArchiveWriter(
        IFileService fileService,
        IPathService pathService,
        IDirectoryService directoryService,
        ArchiveType archiveType,
        WriterOptions options)
    {
        this._fileService = fileService;
        this._pathService = pathService;
        this._directoryService = directoryService;
        this._archiveType = archiveType;
        this._options = options;
    }

    /// <inheritdoc />
    public async Task PackAsync(IReadOnlyList<string> files, IReadOnlyList<string> directories,
        string sourceDirectory, string outputFile)
    {
        await using var outStream = this._fileService.OpenWrite(outputFile);
        using var writer = WriterFactory.Open(outStream, this._archiveType, this._options);

        var allFiles = files.Concat(directories.SelectMany(d => this._directoryService.GetFilesRecursively(d)));

        foreach (var file in allFiles)
        {
            var entryPath = this._pathService.GetRelativePath(sourceDirectory, file);
            writer.Write(entryPath, file);
        }
    }
}