namespace RocketBlend.Services.Abstractions.Applications;

/// <summary>
/// The application version provider.
/// </summary>
public interface IApplicationVersionProvider
{
    /// <summary>
    /// Gets the version.
    /// </summary>
    string Version { get; }
}