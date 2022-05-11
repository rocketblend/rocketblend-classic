using System.Diagnostics;

namespace RocketBlend.Application.Interfaces;

/// <summary>
/// The file service.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Opens the file.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="args">The args.</param>
    public void OpenFile(string path, string args = "");

    /// <summary>
    /// Opens the file.
    /// </summary>
    /// <param name="processStartInfo">The process start info.</param>
    public void OpenFile(ProcessStartInfo processStartInfo);
}
