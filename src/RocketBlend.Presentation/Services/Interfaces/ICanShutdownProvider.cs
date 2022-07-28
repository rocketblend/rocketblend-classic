namespace RocketBlend.Presentation.Services.Interfaces;

/// <summary>
/// The can shutdown provider.
/// </summary>
public interface ICanShutdownProvider
{
    /// <summary>
    /// Cans the shutdown.
    /// </summary>
    /// <returns>A bool.</returns>
    bool CanShutdown();
}