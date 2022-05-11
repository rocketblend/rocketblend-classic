using RocketBlend.Application.Queries.Installs;

namespace RocketBlend.Core.ViewModels.Installs;

/// <summary>
/// The install view model.
/// </summary>
public class InstallViewModel : ViewModelBase
{
    private readonly InstallDto _install;

    /// <summary>
    /// Initializes a new instance of the <see cref="InstallViewModel"/> class.
    /// </summary>
    /// <param name="install">The install.</param>
    public InstallViewModel(InstallDto install)
    {
        this._install = install;
    }

    /// <inheritdoc />
    public string Id => this._install.Id.ToString();

    /// <inheritdoc />
    public string FileName => this._install.FileName;

    /// <inheritdoc />
    public string FileLocation => this._install.FileLocation;
}
