namespace RocketBlend.Application.Exceptions;

/// <summary>
/// The forbidden access exception.
/// </summary>
public class FileFailedToLaunchException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileFailedToLaunchException"/> class.
    /// </summary>
    public FileFailedToLaunchException() : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileFailedToLaunchException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public FileFailedToLaunchException(string message)
    : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FileFailedToLaunchException"/> class.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <param name="innerException">The inner exception.</param>
    public FileFailedToLaunchException(string message, Exception innerException)
    : base(message, innerException)
    {
    }
}
