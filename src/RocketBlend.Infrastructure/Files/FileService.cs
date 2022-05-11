using System.Diagnostics;
using RocketBlend.Application.Exceptions;
using RocketBlend.Application.Interfaces;
using RocketBlend.Common.CrossCuttingConcerns.Guards;

namespace RocketBlend.Infrastructure.Files;

/// <summary>
/// Used to run files.
/// </summary>
public class FileService : IFileService
{
    /// <inheritdoc />
    public void OpenFile(string path, string args = "")
    {
        Guard.ArgumentNotNullOrWhiteSpace(path, nameof(path));

        ProcessStartInfo startInfo = new(path)
        {
            Arguments = args,
            UseShellExecute = true
        };

        this.OpenFile(startInfo);
    }

    /// <inheritdoc />
    public void OpenFile(ProcessStartInfo processStartInfo)
    {
        try
        {
            Process.Start(processStartInfo);
        }
        catch (Exception e)
        {
            throw new FileFailedToLaunchException("File failed to launch!", e);
        }
    }
}
