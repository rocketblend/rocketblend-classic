using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Archive;
using RocketBlend.Services.Archives.Interfaces;

namespace RocketBlend.Services.Archives;

/// <summary>
/// The single file zip archive reader.
/// </summary>
public class SingleFileZipArchiveReader : IArchiveReader
{
    private readonly IFileService _fileService;
    private readonly IFileNameGenerationService _fileNameGenerationService;
    private readonly IPathService _pathService;
    private readonly IStreamFactory _streamFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="SingleFileZipArchiveReader"/> class.
    /// </summary>
    /// <param name="fileService">The file service.</param>
    /// <param name="fileNameGenerationService">The file name generation service.</param>
    /// <param name="pathService">The path service.</param>
    /// <param name="streamFactory">The stream factory.</param>
    public SingleFileZipArchiveReader(
        IFileService fileService,
        IFileNameGenerationService fileNameGenerationService,
        IPathService pathService,
        IStreamFactory streamFactory)
    {
        this._fileService = fileService;
        this._fileNameGenerationService = fileNameGenerationService;
        this._pathService = pathService;
        this._streamFactory = streamFactory;
    }

    /// <inheritdoc />
    public async Task ExtractAsync(string archivePath, string outputDirectory)
    {
        await using var inputStream = this._fileService.OpenRead(archivePath);
        await using var zipStream = this._streamFactory.Create(inputStream);

        var outputFilePath = this.GetOutputFilePath(archivePath, outputDirectory);
        await using var outputStream = this._fileService.OpenWrite(outputFilePath);

        await zipStream.CopyToAsync(outputStream);
    }

    /// <summary>
    /// Gets the output file path.
    /// </summary>
    /// <param name="archivePath">The archive path.</param>
    /// <param name="outputDirectory">The output directory.</param>
    /// <returns>A string.</returns>
    private string GetOutputFilePath(string archivePath, string outputDirectory)
    {
        var archiveFileName = this._pathService.GetFileNameWithoutExtension(archivePath);
        var outputFilePath = this._pathService.Combine(outputDirectory, archiveFileName);

        return this._fileNameGenerationService.GenerateFullName(outputFilePath);
    }
}