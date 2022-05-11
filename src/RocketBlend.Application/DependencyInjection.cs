using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RocketBlend.Common.Application.Behaviours;
using RocketBlend.Common.Application.Interfaces;
using RocketBlend.Common.Application.Mappings;
using RocketBlend.Common.Application.Services;
using System.Reflection;

namespace RocketBlend.Application;

/// <summary>
/// The dependency injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the application services.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudService<,>), typeof(CrudService<,>));

        // Auto mapper
        services.AddAutoMapper(typeof(MappingProfile));

        // Adds Fluent validators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Add MediatR
        services.AddMediatR(Assembly.GetExecutingAssembly());

        // Adds the transient pipeline behavior and additionally registers all `IAuthorizationHandlers` for a given assembly
        //services.AddMediatorAuthorization(Assembly.GetExecutingAssembly());
        // Register all `IAuthorizer` implementations for a given assembly
        //services.AddAuthorizersFromAssembly(Assembly.GetExecutingAssembly());

        // Add other transient pipelines
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        return services;
    }
}
