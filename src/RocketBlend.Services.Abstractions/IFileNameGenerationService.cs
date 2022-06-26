namespace RocketBlend.Services.Abstractions;

/// <summary>
/// The file name generation service.
/// </summary>
public interface IFileNameGenerationService
{
    /// <summary>
    /// Generates the full name.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>A string.</returns>
    string GenerateFullName(string filePath);

    /// <summary>
    /// Generates the full name without extension.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns>A string.</returns>
    string GenerateFullNameWithoutExtension(string filePath);

    /// <summary>
    /// Generates the name.
    /// </summary>
    /// <param name="initialName">The initial name.</param>
    /// <param name="directory">The directory.</param>
    /// <returns>A string.</returns>
    string GenerateName(string initialName, string directory);
}
