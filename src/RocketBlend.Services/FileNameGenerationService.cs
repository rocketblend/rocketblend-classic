using RocketBlend.Services.Abstractions;

namespace RocketBlend.Services;

/// <summary>
/// The file name generation service.
/// </summary>
public class FileNameGenerationService : IFileNameGenerationService
{
    private readonly INodeService _nodeService;
    private readonly IPathService _pathService;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileNameGenerationService"/> class.
    /// </summary>
    /// <param name="nodeService">The node service.</param>
    /// <param name="pathService">The path service.</param>
    public FileNameGenerationService(
        INodeService nodeService,
        IPathService pathService)
    {
        this._nodeService = nodeService;
        this._pathService = pathService;
    }

    /// <summary>
    /// Generates the full name.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>A string.</returns>
    public string GenerateFullName(string filePath)
    {
        var initialName = this._pathService.GetFileName(filePath);

        return this.GenerateByInitialName(filePath, initialName);
    }

    /// <summary>
    /// Generates the full name without extension.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>A string.</returns>
    public string GenerateFullNameWithoutExtension(string filePath)
    {
        var initialName = this._pathService.GetFileNameWithoutExtension(filePath);

        return this.GenerateByInitialName(filePath, initialName);
    }

    /// <summary>
    /// Generates the name.
    /// </summary>
    /// <param name="initialName">The initial name.</param>
    /// <param name="directory">The directory.</param>
    /// <returns>A string.</returns>
    public string GenerateName(string initialName, string directory)
    {
        var currentName = initialName;
        for (var i = 1; this.IsNameAlreadyInUse(currentName, directory); i++)
        {
            currentName = this.GenerateNewName(initialName, i);
        }

        return currentName;
    }

    /// <summary>
    /// Generates the by initial name.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="initialName">The initial name.</param>
    /// <returns>A string.</returns>
    private string GenerateByInitialName(string filePath, string initialName)
    {
        var directory = this._pathService.GetParentDirectory(filePath);
        var newName = this.GenerateName(initialName, directory);

        return this._pathService.Combine(directory, newName);
    }

    /// <summary>
    /// Are the name already in use.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="directory">The directory.</param>
    /// <returns>A bool.</returns>
    private bool IsNameAlreadyInUse(string name, string directory)
    {
        var fullPath = this._pathService.Combine(directory, name);

        return this._nodeService.CheckIfExists(fullPath);
    }

    /// <summary>
    /// Generates the new name.
    /// </summary>
    /// <param name="currentName">The current name.</param>
    /// <param name="i">The i.</param>
    /// <returns>A string.</returns>
    private string GenerateNewName(string currentName, int i)
    {
        var fileName = this._pathService.GetFileNameWithoutExtension(currentName);
        var extension = this._pathService.GetExtension(currentName);

        return string.IsNullOrEmpty(extension) ? $"{fileName} ({i})" : $"{fileName} ({i}).{extension}";
    }
}