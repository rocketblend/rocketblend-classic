namespace RocketBlend.Core.Utils.Interfaces;

/// <summary>
/// The application info interface.
/// </summary>
public interface IApplicationInfo
{
    /// <summary>
    /// Gets the name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the organization.
    /// </summary>
    string Organization { get; }

    /// <summary>
    /// Gets the copyright.
    /// </summary>
    string Copyright { get; }

    /// <summary>
    /// Gets the version.
    /// </summary>
    string Version { get; }

    /// <summary>
    /// Gets the file version.
    /// </summary>
    string FileVersion { get; }

    /// <summary>
    /// Gets the created.
    /// </summary>
    DateTime Created { get; }
}
