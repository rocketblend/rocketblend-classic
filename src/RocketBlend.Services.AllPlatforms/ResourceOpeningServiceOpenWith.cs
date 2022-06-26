using RocketBlend.Services.Abstractions;

namespace RocketBlend.Services.AllPlatforms;

/// <summary>
/// The resource opening service open with.
/// </summary>
public class ResourceOpeningServiceOpenWith : IResourceOpeningService
{
    private readonly IPathService _pathService;
    private readonly IResourceOpeningService _resourceOpeningService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResourceOpeningServiceOpenWith"/> class.
    /// </summary>
    /// <param name="resourceOpeningService">The resource opening service.</param>
    /// <param name="pathService">The path service.</param>
    public ResourceOpeningServiceOpenWith(
        IResourceOpeningService resourceOpeningService,
        IPathService pathService)
    {
        this._resourceOpeningService = resourceOpeningService;
        this._pathService = pathService;
    }

    /// <inheritdoc />
    public void Open(string resource)
    {
        //var extension = this._pathService.GetExtension(resource);
        //var selectedApplication = this._openWithApplicationService.GetSelectedApplication(extension);

        //if (selectedApplication is null)
        //{
        //    this._resourceOpeningService.Open(resource);
        //}
        //else
        //{
        //    this._resourceOpeningService.OpenWith(selectedApplication.ExecutePath, selectedApplication.Arguments, resource);
        //}

        this._resourceOpeningService.Open(resource);
    }

    /// <inheritdoc />
    public void OpenWith(string command, string arguments, string resource) =>
        this._resourceOpeningService.OpenWith(command, arguments, resource);
}
