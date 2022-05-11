using System.Reflection;
using ReadyAbout.Services.Resource.Infrastructure.Persistence;
using RocketBlend.Core.Services.Interfaces;
using RocketBlend.Core.Utils;
using RocketBlend.Core.Utils.Interfaces;
using Splat;

namespace RocketBlend;

/// <summary>
/// The boot strap class.
/// </summary>
public static class Bootstrapper
{
    /// <summary>
    /// Adds the application.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="app">The app.</param>
    public static void AddApplication(this IMutableDependencyResolver services, IApplication app)
    {
        services.RegisterConstant(app);
    }

    /// <summary>
    /// Adds the application info.
    /// </summary>
    /// <param name="services">The services.</param>
    public static void AddApplicationInfo(this IMutableDependencyResolver services)
    {
        services.RegisterLazySingleton<IApplicationInfo>(() =>
        {
            return new ApplicationInfo(Assembly.GetExecutingAssembly());
        });
    }

    /// <summary>
    /// Configures the database.
    /// </summary>
    /// <param name="services">The services.</param>
    public static void ConfigureDatabase(this IReadonlyDependencyResolver services)
    {
        var db = services.GetRequiredService<ApplicationDbContext>();
        // db.Database.Migrate();
    }
}
 