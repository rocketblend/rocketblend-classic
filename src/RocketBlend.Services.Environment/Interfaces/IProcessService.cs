namespace RocketBlend.Services.Environment.Interfaces;

/// <summary>
/// The process service.
/// </summary>
public interface IProcessService
{
    /// <summary>
    /// Runs the.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="arguments">The arguments.</param>
    void Run(string command, string arguments);

    /// <summary>
    /// Executes the and get output async.
    /// </summary>
    /// <param name="command">The command.</param>
    /// <param name="arguments">The arguments.</param>
    /// <returns>A Task.</returns>
    Task<string> ExecuteAndGetOutputAsync(string command, string arguments);
}