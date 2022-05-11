using Microsoft.EntityFrameworkCore;
using RocketBlend.Common.Application.Interfaces;
using RocketBlend.Infrastructure.Persistence;
using System.Reflection;

namespace ReadyAbout.Services.Resource.Infrastructure.Persistence;

/// <summary>
/// The app db context.
/// </summary>
public class ApplicationDbContext : DbContextUnitOfWork<ApplicationDbContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="domainEventService">The domain event service.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDomainEventService domainEventService)
        : base(options, domainEventService)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}