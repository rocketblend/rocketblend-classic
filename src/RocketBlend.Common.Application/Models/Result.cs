namespace RocketBlend.Common.Application.Models;

/// <summary>
/// The result.
/// </summary>
public class Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="succeeded">If true, succeeded.</param>
    /// <param name="errors">The errors.</param>
    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        this.Succeeded = succeeded;
        this.Errors = errors.ToArray();
    }

    /// <summary>
    /// Gets or sets a value indicating whether succeeded.
    /// </summary>
    public bool Succeeded { get; set; }

    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    public string[] Errors { get; set; }

    /// <summary>
    /// Successes the.
    /// </summary>
    /// <returns>A Result.</returns>
    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    /// <summary>
    /// Failures the.
    /// </summary>
    /// <param name="errors">The errors.</param>
    /// <returns>A Result.</returns>
    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }
}
