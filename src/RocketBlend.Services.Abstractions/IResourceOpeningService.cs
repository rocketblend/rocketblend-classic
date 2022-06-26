namespace RocketBlend.Services.Abstractions;

/// <summary>
/// The resource opening service.
/// </summary>
public interface IResourceOpeningService
{
    /// <summary>
    /// Opens the.
    /// </summary>
    /// <param name="resource">The resource.</param>
    void Open(string resource);

    /// <summary>
    /// Opens the with.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="arguments">The arguments.</param>
    /// <param name="resource">The resource.</param>
    void OpenWith(string command, string arguments, string resource);
}