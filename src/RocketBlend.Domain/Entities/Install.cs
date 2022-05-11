using RocketBlend.Common.Domain.Entities;
using RocketBlend.Domain.Events.Installs;

namespace RocketBlend.Domain.Entities;

/// <summary>
/// The install.
/// </summary>
public class Install : AggregateRoot<Guid>, IFileEntry<Guid>
{
    /// <summary>
    /// EF constructor
    /// Initializes a new instance of the <see cref="Install"/> class.
    /// </summary>
    internal Install() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Install"/> class.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="buildId">The build id.</param>
    /// <param name="fileName">The file name.</param>
    /// <param name="fileLocation">The file location.</param>
    /// <param name="launchArgs">The launch args.</param>
    public Install(Guid id, Guid buildId, string fileName, string fileLocation, string launchArgs)
    {
        this.Id = id;
        this.BuildId = buildId;
        this.FileName = fileName;
        this.FileLocation = fileLocation;
        this.LaunchArgs = launchArgs;

        this.AddDomainEvent(new InstallCreatedEvent(this));
    }

    /// <summary>
    /// Gets the build id.
    /// </summary>
    public Guid BuildId { get; private set; }

    /// <inheritdoc />
    public string FileName { get; private set; } = string.Empty;

    /// <inheritdoc />
    public string FileLocation { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the launch args.
    /// </summary>
    public string LaunchArgs { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the build.
    /// </summary>
    public virtual Build? Build { get; private set; }

    /// <summary>
    /// Gets the projects navigation.
    /// </summary>
    public virtual ICollection<Project> Projects { get; private set; } = new List<Project>();
}
