using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Archive;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Archive;

public class ArchiveService : IArchiveService
{
    private readonly IArchiveTypeMapper _archiveTypeMapper;
    private readonly IPathService _pathService;
    private readonly IOperationsService _operationsService;
    private readonly IFileNameGenerationService _fileNameGenerationService;

    public ArchiveService(
        IArchiveTypeMapper archiveTypeMapper,
        IPathService pathService,
        IOperationsService operationsService,
        IFileNameGenerationService fileNameGenerationService)
    {
        this._archiveTypeMapper = archiveTypeMapper;
        this._pathService = pathService;
        this._operationsService = operationsService;
        this._fileNameGenerationService = fileNameGenerationService;
    }

    public Task PackAsync(IReadOnlyList<string> nodes, string outputFile, ArchiveType archiveType) =>
        this._operationsService.PackAsync(nodes, outputFile, archiveType);

    public async Task ExtractToNewDirectoryAsync(string archivePath)
    {
        var cleanedUpArchivePath = CleanupArchivePath(archivePath);
        var fullName = this._fileNameGenerationService.GenerateFullNameWithoutExtension(cleanedUpArchivePath);

        await this.ExtractAsync(archivePath, fullName);
    }

    public async Task ExtractAsync(string archivePath, string outputDirectory = null)
    {
        if (!this.CheckIfNodeIsArchive(archivePath))
        {
            throw new InvalidOperationException($"{archivePath} is not an archive!");
        }

        outputDirectory ??= this._pathService.GetParentDirectory(archivePath);
        // ReSharper disable once PossibleInvalidOperationException
        var archiveType = this._archiveTypeMapper.GetArchiveTypeFrom(archivePath).Value;

        await this._operationsService.ExtractAsync(archivePath, outputDirectory, archiveType);
    }

    public bool CheckIfNodeIsArchive(string nodePath) =>
        this._archiveTypeMapper.GetArchiveTypeFrom(nodePath).HasValue;

    private static string CleanupArchivePath(string archivePath) =>
        archivePath.Replace(".tar", string.Empty);
}