using RocketBlend.Services.Abstractions.Models;

namespace RocketBlend.Services.Abstractions;

/// <summary>
/// The node service.
/// </summary>
public interface INodeService
{
    /// <summary>
    /// Checks the if exists.
    /// </summary>
    /// <param name="nodePath">The node path.</param>
    /// <returns>A bool.</returns>
    bool CheckIfExists(string nodePath);

    /// <summary>
    /// Gets the node.
    /// </summary>
    /// <param name="nodePath">The node path.</param>
    /// <returns>A NodeModelBase.</returns>
    NodeModelBase GetNode(string nodePath);
}
