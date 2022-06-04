using RocketBlend.Presentation.Services;
using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Presentation.ViewModels.Dialogs.Results;

/// <summary>
/// The select builds dialog result.
/// </summary>
public class SelectBuildsDialogResult : DialogResultBase
{
    /// <summary>
    /// Gets the builds.
    /// </summary>
    public IReadOnlyList<BlenderBuildModel> Builds { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectBuildsDialogResult"/> class.
    /// </summary>
    /// <param name="builds">The builds.</param>
    public SelectBuildsDialogResult(IReadOnlyList<BlenderBuildModel> builds)
    {
        this.Builds = builds;
    }
}
