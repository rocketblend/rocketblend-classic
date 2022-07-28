namespace RocketBlend.Services.Abstractions.Applications;

/// <summary>
/// The application interface.
/// </summary>
public interface IApplicationCloser
{
    /// <summary>
    /// Shutdown the application.
    /// </summary>
    void Shutdown();
}