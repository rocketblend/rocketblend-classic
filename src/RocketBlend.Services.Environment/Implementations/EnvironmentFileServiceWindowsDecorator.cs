using RocketBlend.Services.Environment.Interfaces;

namespace RocketBlend.Services.Environment.Implementations;

/// <summary>
/// The environment file service windows decorator.
/// </summary>
public class EnvironmentFileServiceWindowsDecorator : IEnvironmentFileService
{
    private readonly IEnvironmentFileService _environmentFileService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnvironmentFileServiceWindowsDecorator"/> class.
    /// </summary>
    /// <param name="environmentFileService">The environment file service.</param>
    public EnvironmentFileServiceWindowsDecorator(IEnvironmentFileService environmentFileService)
    {
        this._environmentFileService = environmentFileService;
    }

    /// <inheritdoc />
    public FileInfo GetFile(string file) => this._environmentFileService.GetFile(file);

    /// <inheritdoc />
    public string[] GetFiles(string directory) =>
        this._environmentFileService.GetFiles(PreprocessPath(directory));

    /// <inheritdoc />
    public bool CheckIfExists(string filePath) => this._environmentFileService.CheckIfExists(filePath);

    /// <inheritdoc />
    public void Move(string oldFilePath, string newFilePath) => this._environmentFileService.Move(oldFilePath, newFilePath);

    /// <inheritdoc />
    public void Delete(string filePath) => this._environmentFileService.Delete(filePath);

    /// <inheritdoc />
    public Task WriteTextAsync(string filePath, string text) => this._environmentFileService.WriteTextAsync(filePath, text);

    /// <inheritdoc />
    public Task WriteBytesAsync(string filePath, byte[] bytes) => this._environmentFileService.WriteBytesAsync(filePath, bytes);

    /// <inheritdoc />
    public void Create(string filePath) => this._environmentFileService.Create(filePath);

    /// <inheritdoc />
    public Stream OpenRead(string filePath) => this._environmentFileService.OpenRead(filePath);

    /// <inheritdoc />
    public Stream OpenWrite(string filePath) => this._environmentFileService.OpenWrite(filePath);

    /// <inheritdoc />
    private static string PreprocessPath(string directory) =>
        directory.EndsWith("\\") ? directory : directory + "\\";
}