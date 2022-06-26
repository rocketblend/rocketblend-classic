using RocketBlend.Services.Abstractions;
using RocketBlend.Services.Environment.Interfaces;

namespace RocketBlend.Services.Windows;

/// <summary>
/// The windows resource opening service.
/// </summary>
public class WindowsResourceOpeningService : IResourceOpeningService
{
    private readonly IProcessService _processService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WindowsResourceOpeningService"/> class.
    /// </summary>
    /// <param name="processService">The process service.</param>
    public WindowsResourceOpeningService(IProcessService processService)
    {
        this._processService = processService;
    }

    /// <inheritdoc />
    public void Open(string resource) => this.OpenWith("explorer", "{0}", resource);

    /// <inheritdoc />
    public void OpenWith(string command, string arguments, string resource)
    {
        if (resource.Any(char.IsWhiteSpace))
        {
            resource = "\"" + resource + "\"";
        }

        this._processService.Run(command, string.Format(arguments, resource).TrimStart());
    }
}
