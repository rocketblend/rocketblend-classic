namespace RocketBlend.Services.Abstractions;

/// <summary>
/// The launch argument service.
/// </summary>
public interface ILaunchArgumentService
{
    /// <summary>
    /// Handles the arguments.
    /// </summary>
    /// <param name="args">The args.</param>
    void HandleArguments(string[] args);
}
