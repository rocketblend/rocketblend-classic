using RocketBlend.Blender.Interfaces;
using CliWrap;
using System.Text.RegularExpressions;

namespace RocketBlend.Blender;

/// <summary>
/// The blender client.
/// </summary>
public class BlenderClient: IBlenderClient
{
    /// <summary>
    /// Gets the client.
    /// </summary>
    private Command Client => Cli.Wrap(this.Executable).WithWorkingDirectory(this.WorkingDirectory);

    /// <inheritdoc />
    public string WorkingDirectory { get; set; } = string.Empty;

    /// <inheritdoc />
    public string Executable { get; set; } = "blender.exe";

    /// <inheritdoc />
    public async void OpenProject(string path)
    {
        await this.Client.WithArguments(EscapeArguments(path)).ExecuteAsync();
    }

    /// <inheritdoc />
    public void RenderAnimation(string path, string output, int? startFrame = null, int? endframe = null, int? threads = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void RenderSingleImage(string path, string output, int? frame = null, string? format = null)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Escapes the arguments.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    /// <returns>A string.</returns>
    private static string EscapeArguments(string arguments)
    {
        return Regex.Replace(arguments, @"^(.*\s.*?)(\\*)$", "\"$1$2$2\"");
    }
}
