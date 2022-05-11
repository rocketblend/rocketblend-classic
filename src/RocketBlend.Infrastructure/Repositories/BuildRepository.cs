using Microsoft.EntityFrameworkCore;
using ReadyAbout.Services.Resource.Infrastructure.Persistence;
using RocketBlend.Common.CrossCuttingConcerns.OS;
using RocketBlend.Domain.Entities;
using RocketBlend.Domain.Repositories;

namespace RocketBlend.Infrastructure.Repositories;

/// <summary>
/// The build repository.
/// </summary>
public class BuildRepository : BaseRepository<Build, Guid>, IBuildRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BuildRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The db context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    public BuildRepository(ApplicationDbContext dbContext,
        IDateTimeProvider dateTimeProvider)
        : base(dbContext, dateTimeProvider)
    {
    }

    /// <inheritdoc />
    public IQueryable<Build> Get(BuildQueryOptions queryOptions)
    {
        var query = this.GetAll();

        if (queryOptions.AsNoTracking)
        {
            query = query.AsNoTracking();
        }

        query = query.OrderBy(p => p.Name);

        return query;
    }
}
