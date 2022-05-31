namespace RocketBlend.Services.Environment.Interfaces;

/// <summary>
/// The environment service.
/// </summary>
public interface IEnvironmentService
{
    /// <summary>
    /// Gets the new line.
    /// </summary>
    string NewLine { get; }

    /// <summary>
    /// Gets a value indicating whether is64 bit process.
    /// </summary>
    bool Is64BitProcess { get; }

    /// <summary>
    /// Gets the environment variable.
    /// </summary>
    /// <param name="variableName">The variable name.</param>
    /// <returns>A string.</returns>
    string GetEnvironmentVariable(string variableName);
}