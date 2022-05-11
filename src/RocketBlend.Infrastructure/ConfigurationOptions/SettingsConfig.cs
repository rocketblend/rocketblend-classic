namespace RocketBlend.Application.ConfigurationOptions;

/// <summary>
/// The app settings.
/// </summary>
public class SettingsConfig
{
    /// <summary>
    /// Gets or sets the connection strings.
    /// </summary>
    public ConnectionStrings ConnectionStrings { get; set; } = new();
}

/// <summary>
/// The connection strings.
/// </summary>
public class ConnectionStrings
{
    /// <summary>
    /// Gets or sets the db connection.
    /// </summary>
    public string? DbConnection { get; set; } = "Data Source=rocketblend.db;Mode=ReadWriteCreate;";

    /// <summary>
    /// Gets or sets the migrations assembly.
    /// </summary>
    public string? MigrationsAssembly { get; set; } = string.Empty;
}
