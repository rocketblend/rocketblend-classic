using RocketBlend.Common.Application;

namespace RocketBlend.Infrastructure.Identity;

/// <summary>
/// The current local user.
/// </summary>
public class CurrentLocalUser : ICurrentUser
{
    /// <inheritdoc />
    public bool IsAuthenticated => true;

    /// <inheritdoc />
    public Guid UserId => Guid.Empty;

    /// <inheritdoc />
    public string UserName => "LOCAL";
}
