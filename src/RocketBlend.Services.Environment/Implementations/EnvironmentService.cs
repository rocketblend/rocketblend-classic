using RocketBlend.Services.Environment.Interfaces;
using SysEnv = System.Environment;

namespace RocketBlend.Services.Environment.Implementations;

/// <summary>
/// The environment service.
/// </summary>
public class EnvironmentService : IEnvironmentService
{
    /// <inheritdoc />
    public string NewLine => SysEnv.NewLine;

    /// <inheritdoc />
    public bool Is64BitProcess => SysEnv.Is64BitProcess;

    /// <inheritdoc />
    public string GetEnvironmentVariable(string variableName) =>
        SysEnv.GetEnvironmentVariable(variableName);
}