using RocketBlend.Services.Environment.Interfaces;

namespace RocketBlend.Services.Environment.Implementations;

/// <summary>
/// The environment file service.
/// </summary>
public class EnvironmentFileService : IEnvironmentFileService
{
    /// <inheritdoc />
    public FileInfo GetFile(string file) =>
        new(file);

    /// <inheritdoc />
    public string[] GetFiles(string directory) =>
        Directory.GetFiles(directory);

    /// <inheritdoc />
    public bool CheckIfExists(string filePath) =>
        File.Exists(filePath);

    /// <inheritdoc />
    public void Move(string oldFilePath, string newFilePath) =>
        File.Move(oldFilePath, newFilePath);

    /// <inheritdoc />
    public void Delete(string filePath) =>
        File.Delete(filePath);

    /// <inheritdoc />
    public Task WriteTextAsync(string filePath, string text) =>
        File.WriteAllTextAsync(filePath, text);

    /// <inheritdoc />
    public Task WriteBytesAsync(string filePath, byte[] bytes) =>
        File.WriteAllBytesAsync(filePath, bytes);

    /// <inheritdoc />
    public void Create(string filePath) =>
        File.Create(filePath).Dispose();

    /// <inheritdoc />
    public Stream OpenRead(string filePath) =>
        File.OpenRead(filePath);

    /// <inheritdoc />
    public Stream OpenWrite(string filePath) =>
        File.OpenWrite(filePath);
}