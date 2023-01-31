using System;
using System.IO;
using System.Runtime.InteropServices;

namespace RocketBlend.Presentation.Avalonia.Services;

/// <summary>
/// The single instance helper.
/// </summary>
public static class SingleInstanceHelper
{
    static FileStream? _lockFile;

    /// <summary>
    /// Ensures the not already running.
    /// </summary>
    /// <param name="startupAction">The startup action.</param>
    /// <returns>An int.</returns>
    public static int EnsureNotAlreadyRunning(Func<int> startupAction)
    {
        // OSX does this by default.
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RocketBlend", "RocketBlend");
            Directory.CreateDirectory(dir);
            try
            {
                _lockFile = File.Open(Path.Combine(dir, ".lock"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                _lockFile.Lock(0, 0);
            }
            catch
            {
                return -1;
            }
        }
        
        return startupAction();
    }
}
