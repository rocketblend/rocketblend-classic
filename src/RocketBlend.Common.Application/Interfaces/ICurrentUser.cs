namespace RocketBlend.Common.Application;
/// <summary>
/// The current user.
/// </summary>

public interface ICurrentUser
{
    /// <summary>
    /// Gets a value indicating whether is authenticated.
    /// </summary>
    bool IsAuthenticated { get; }

    /// <summary>
    /// Gets the user id.
    /// </summary>
    Guid UserId { get; }

    /// <summary>
    /// Gets the user name.
    /// </summary>
    string UserName { get; }
}
