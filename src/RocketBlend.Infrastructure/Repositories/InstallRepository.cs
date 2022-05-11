using Microsoft.EntityFrameworkCore;
using ReadyAbout.Services.Resource.Infrastructure.Persistence;
using RocketBlend.Common.CrossCuttingConcerns.OS;
using RocketBlend.Domain.Entities;
using RocketBlend.Domain.Repositories;

namespace RocketBlend.Infrastructure.Repositories;

/// <summary>
/// The install repository.
/// </summary>
public class InstallRepository : BaseRepository<Install, Guid>, IInstallRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InstallRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The db context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    public InstallRepository(ApplicationDbContext dbContext,
        IDateTimeProvider dateTimeProvider)
        : base(dbContext, dateTimeProvider)
    {
    }

    /// <inheritdoc />
    public IQueryable<Install> Get(InstallQueryOptions queryOptions)
    {
        var query = this.GetAll();

        if (queryOptions.AsNoTracking)
        {
            query = query.AsNoTracking();
        }

        query = query.OrderBy(p => p.FileName);

        return query;
    }
}
