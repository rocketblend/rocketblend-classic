using ReadyAbout.Services.Resource.Infrastructure.Persistence;
using RocketBlend.Common.CrossCuttingConcerns.OS;
using RocketBlend.Common.Domain.Entities;
using RocketBlend.Infrastructure.Persistence;

namespace RocketBlend.Infrastructure.Repositories;

/// <summary>
/// The base catalog repository.
/// </summary>
public class BaseRepository<T, TKey> : DbContextRepository<ApplicationDbContext, T, TKey>
        where T : AggregateRoot<TKey>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BaseExampleRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The db context.</param>
    /// <param name="dateTimeProvider">The date time provider.</param>
    public BaseRepository(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
        : base(dbContext, dateTimeProvider)
    {
    }
}