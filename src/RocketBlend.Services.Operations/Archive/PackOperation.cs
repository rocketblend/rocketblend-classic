using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Abstractions.Archive;
using RocketBlend.Services.Abstractions.Models.Enums;
using RocketBlend.Services.Abstractions.Models.Operations;
using RocketBlend.Services.Abstractions.Operations;

namespace RocketBlend.Services.Operations.Archive;

/// <summary>
/// The pack operation.
/// </summary>
public class PackOperation : StatefulOperationWithProgressBase, IInternalOperation
{
    private readonly IArchiveWriter _archiveWriter;
    private readonly IDirectoryService _directoryService;
    private readonly IPathService _pathService;
    private readonly PackOperationSettings _settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="PackOperation"/> class.
    /// </summary>
    /// <param name="archiveWriter">The archive writer.</param>
    /// <param name="directoryService">The directory service.</param>
    /// <param name="pathService">The path service.</param>
    /// <param name="settings">The settings.</param>
    public PackOperation(
        IArchiveWriter archiveWriter,
        IDirectoryService directoryService,
        IPathService pathService,
        PackOperationSettings settings)
    {
        this._archiveWriter = archiveWriter;
        this._directoryService = directoryService;
        this._pathService = pathService;
        this._settings = settings;
    }

    /// <inheritdoc />
    public async Task RunAsync(CancellationToken cancellationToken)
    {
        this.CreateOutputDirectoryIfNeeded(this._settings.TargetDirectory);

        this.State = OperationState.InProgress;
        await this._archiveWriter.PackAsync(this._settings.InputTopLevelFiles, this._settings.InputTopLevelDirectories,
            this._settings.SourceDirectory, this._settings.OutputTopLevelFile);
        this.State = OperationState.Finished;
        this.SetFinalProgress();
    }

    /// <summary>
    /// Creates the output directory if needed.
    /// </summary>
    /// <param name="outputFilePath">The output file path.</param>
    private void CreateOutputDirectoryIfNeeded(string outputFilePath)
    {
        var outputDirectory = this._pathService.GetParentDirectory(outputFilePath);
        if (!this._directoryService.CheckIfExists(outputDirectory))
        {
            this._directoryService.Create(outputDirectory);
        }
    }
}