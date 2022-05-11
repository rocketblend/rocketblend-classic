using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ReadyAbout.Services.Resource.Infrastructure.Persistence;

namespace RocketBlend.Infrastructure.Persistence;

/// <summary>
///     Used for design time migrations.  
///     Will look to the appsettings.json file in this project for the connection string.
///     EF Core tools scans the assembly containing the dbcontext for an implementation
///     of IDesignTimeDbContextFactory.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    /// <summary>
    /// Creates the db context. This is only used for design time migrations.
    /// </summary>
    /// <param name="args">The args.</param>
    /// <returns>An ApplicationDbContext.</returns>
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        //IConfigurationBuilder builder =
        // new ConfigurationBuilder()
        //     .SetBasePath(path)
        //     .AddJsonFile("appsettings.json");

        //IConfigurationRoot config = builder.Build();

        //string connectionString = config.GetConnectionString("DbConnection");

        string connection = "Data Source=RocketBlend.db;Mode=ReadWriteCreate;Version=3;";
        string migrationsAssembly = string.Empty;

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlite(connection, sql =>
        {
            sql.CommandTimeout((int)TimeSpan.FromMinutes(3).TotalSeconds);

            if (!string.IsNullOrEmpty(migrationsAssembly))
            {
                sql.MigrationsAssembly(migrationsAssembly);
            }
            else
            {
                sql.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            }
        });

        return new ApplicationDbContext(optionsBuilder.Options, null!); // Passing null so we don't have to define domain events service.
    }
}
