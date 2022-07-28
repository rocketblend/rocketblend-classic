using System.Diagnostics;
using RocketBlend.Services.Environment.Interfaces;

namespace RocketBlend.Services.Environment.Implementations;

/// <summary>
/// The process service.
/// </summary>
public class ProcessService : IProcessService
{
    /// <inheritdoc />
    public void Run(string command, string arguments)
    {
        var process = GetProcess(command, arguments);
        process.Start();
    }

    /// <inheritdoc />
    public async Task<string> ExecuteAndGetOutputAsync(string command, string arguments)
    {
        var process = GetProcess(command, arguments, true);
        process.Start();

        return await process.StandardOutput.ReadToEndAsync();
    }

    /// <inheritdoc />
    private static Process GetProcess(string command, string arguments, bool redirectOutput = false)
    {
        var processStartInfo = new ProcessStartInfo(command)
        {
            RedirectStandardOutput = redirectOutput,
            UseShellExecute = false,
            CreateNoWindow = true,
            Arguments = arguments
        };

        return new Process { StartInfo = processStartInfo };
    }
}