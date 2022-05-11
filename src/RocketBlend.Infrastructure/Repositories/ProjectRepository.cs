using Microsoft.EntityFrameworkCore;
using ReadyAbout.Services.Resource.Infrastructure.Persistence;
using RocketBlend.Common.CrossCuttingConcerns.OS;
using RocketBlend.Domain.Entities;
using RocketBlend.Domain.Repositories;

namespace RocketBlend.Infrastructure.Repositories;

/// <summary>
/// The project repository.
/// </summary>
public class ProjectRepository : BaseRepository<Project, Guid>, IProjectRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The db context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    public ProjectRepository(ApplicationDbContext dbContext,
        IDateTimeProvider dateTimeProvider)
        : base(dbContext, dateTimeProvider)
    {
    }

    /// <inheritdoc />
    public IQueryable<Project> Get(ProjectQueryOptions queryOptions)
    {
        var query = this.GetAll();

        if (!string.IsNullOrWhiteSpace(queryOptions.ProjectName))
        {
            query = query.Where(p => EF.Functions.Like(p.Name, $"%{queryOptions.ProjectName}%"));
        }

        if(queryOptions.IncludeInstall)
        {
            query = query.Include(p => p.Install);
        }

        if (queryOptions.AsNoTracking)
        {
            query = query.AsNoTracking();
        }

        query = query.OrderBy(p => p.Name);

        return query;
    }
}
