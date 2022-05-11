using RocketBlend.Common.CrossCuttingConcerns.Guards;
using RocketBlend.Common.Domain.Entities;
using RocketBlend.Domain.Events.Projects;

namespace RocketBlend.Domain.Entities;

/// <summary>
/// The project.
/// </summary>
public class Project : AggregateRoot<Guid>
{
    /// <summary>
    /// EF constructor
    /// Initializes a new instance of the <see cref="Project"/> class.
    /// </summary>
    internal Project() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Project"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="path">The path.</param>
    public Project(Guid id, string name, string path, Guid? installId = null)
    {
        Guard.ArgumentNotNull(id, nameof(id));

        this.Id = id;
        this.Name = name;
        this.Path = path;
        this.InstallId = installId;

        this.AddDomainEvent(new ProjectCreatedEvent(this));
    }

    /// <summary>
    /// Gets or sets the install id.
    /// </summary>
    public Guid? InstallId { get; private set; }

    /// <summary>
    /// Gets the name.
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the path.
    /// </summary>
    public string Path { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the install to run against.
    /// </summary>s
    public virtual Install? Install { get; private set; }

    /// <summary>
    /// Updates the.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="path">The path.</param>
    public void Update(string name, string path)
    {
        Guard.ArgumentNotNullOrWhiteSpace(name, nameof(name));
        Guard.ArgumentNotNullOrWhiteSpace(path, nameof(path));

        this.Name = name;
        this.Path = path;
    }
}