using System.Reflection;
using RocketBlend.Core.Utils.Interfaces;

namespace RocketBlend.Core.Utils;

/// <summary>
/// The application info.
/// </summary>
public class ApplicationInfo : IApplicationInfo
{
    ///<inheritdoc />
    public string Name { get; }

    ///<inheritdoc />
    public string Organization { get; }

    ///<inheritdoc />
    public string Copyright { get; }

    ///<inheritdoc />
    public string Version { get; }

    ///<inheritdoc />
    public string FileVersion { get; }

    ///<inheritdoc />
    public DateTime Created { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationInfo"/> class.
    /// </summary>
    /// <param name="assembly">The assembly.</param>
    public ApplicationInfo(Assembly assembly)
    {
        this.Name = assembly.GetCustomAttribute<AssemblyProductAttribute>()!.Product;
        this.Organization = assembly.GetCustomAttribute<AssemblyCompanyAttribute>()!.Company;
        this.Copyright = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>()!.Copyright;
        this.Version = assembly.GetName().Version!.ToString();
        this.FileVersion = assembly.GetName().Version!.ToString();
        this.Created = DateTime.Now;
    }
}
