using System.Reactive.Linq;
using Akavache;
using DynamicData;
using RocketBlend.Services.Abstractions.Models.Projects;
using RocketBlend.Services.Abstractions.Projects;

namespace RocketBlend.Services.Projects;

/// <summary>
/// The project state service.
/// </summary>
public class ProjectStateService : IProjectStateService
{
    /// <summary>
    /// The projects key.
    /// </summary>
    private const string ProjectsKey = "Projects";

    private readonly IBlobCache _blobCache;

    private readonly SourceCache<ProjectModel, Guid> _items = new(x => x.Id);

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectStateService"/> class.
    /// </summary>
    public ProjectStateService()
    {
        this._blobCache = BlobCache.LocalMachine;

        var installs = this.GetProjects();
        if (installs != null)
        {
            this._items.Edit(cache =>
            {
                cache.Clear();
                cache.AddOrUpdate(installs);
            });
        }
    }

    /// <inheritdoc />
    public IObservable<IChangeSet<ProjectModel, Guid>> Connect() => this._items.Connect();

    /// <inheritdoc />
    public async Task AddOrUpdateProject(ProjectModel project)
    {
        this._items.AddOrUpdate(project);
        await this.Save();
    }

    /// <inheritdoc />
    public async Task RemoveProject(Guid projectId)
    {
        this._items.RemoveKey(projectId);
        await this.Save();
    }

    /// <summary>
    /// Saves the.
    /// </summary>
    /// <returns>A Task.</returns>
    private async Task Save()
    {
        await this._blobCache.InsertObject(ProjectsKey, this._items.Items);
    }

    /// <summary>
    /// Gets the projects.
    /// </summary>
    /// <returns>An IEnumerable&lt;ProjectModel&gt;? .</returns>
    private IEnumerable<ProjectModel>? GetProjects()
    {
        try
        {
            return this._blobCache.GetObject<IEnumerable<ProjectModel>>(ProjectsKey).GetAwaiter().Wait();
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }
}
