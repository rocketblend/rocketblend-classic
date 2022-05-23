using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReadyAbout.Services.Resource.Infrastructure.Persistence;
using RocketBlend.Application.ConfigurationOptions;
using RocketBlend.Application.Interfaces;
using RocketBlend.Common.Application;
using RocketBlend.Common.Caching;
using RocketBlend.Common.Domain.Repositories;
using RocketBlend.Domain.Entities;
using RocketBlend.Domain.Repositories;
using RocketBlend.FileDownloader.DependencyInjectionExtensions;
using RocketBlend.Infrastructure.Files;
using RocketBlend.Infrastructure.Identity;
using RocketBlend.Infrastructure.Repositories;
using RocketBlend.WebScraper.Blender;
using RocketBlend.WebScraper.Blender.Core.Interfaces;

namespace RocketBlend.Infrastructure;

/// <summary>
/// The dependency injection.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the infrastructure.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="appSettings">The app settings.</param>
    /// <returns>An IServiceCollection.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, SettingsConfig appSettings)
    {
        services.AddInMemoryCache();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            options.UseSqlite(appSettings.ConnectionStrings.DbConnection!, sql =>
                {
                    sql.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);

                    if (!string.IsNullOrEmpty(appSettings.ConnectionStrings?.MigrationsAssembly))
                    {
                        sql.MigrationsAssembly(appSettings.ConnectionStrings.MigrationsAssembly);
                    }
                    else
                    {
                        sql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    }
                })
                .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>()), ServiceLifetime.Transient);

        services.AddDateTimeProvider();
        services.AddDomainEventService();

        services.AddFileDownloaderService();
        services.AddZipFileExtractorService();

        services.AddTransient<IRepository<Project, Guid>, BaseRepository<Project, Guid>>();
        services.AddTransient<IProjectRepository, ProjectRepository>();

        services.AddTransient<IRepository<Install, Guid>, BaseRepository<Install, Guid>>();
        services.AddTransient<IInstallRepository, InstallRepository>();

        services.AddTransient<IRepository<Build, Guid>, BaseRepository<Build, Guid>>();
        services.AddTransient<IBuildRepository, BuildRepository>();

        services.AddScoped<ICurrentUser, CurrentLocalUser>();
        services.AddScoped<IFileService, FileService>();

        services.AddScoped<IBlenderBuildScraperService, BlenderBuildScraperService>();

        return services;
    }
}
