namespace RocketBlend.Common.Application.Exceptions;

/// <summary>
/// The forbidden access exception.
/// </summary>
public class ForbiddenAccessException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenAccessException"/> class.
    /// </summary>
    public ForbiddenAccessException() : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ForbiddenAccessException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public ForbiddenAccessException(string? message)
    : base(message)
    {
    }
}
