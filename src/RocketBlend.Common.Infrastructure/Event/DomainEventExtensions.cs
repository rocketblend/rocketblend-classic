using Microsoft.Extensions.DependencyInjection;
using RocketBlend.Common.Application.Interfaces;
using RocketBlend.Common.Infrastructure.Event;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// The domain event extensions.
/// </summary>
public static class DomainEventExtensions
{
    /// <summary>
    /// Adds the domain event service.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddDomainEventService(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventService, DomainEventService>();
        return services;
    }
}
