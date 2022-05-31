using System.Text.RegularExpressions;

namespace RocketBlend.Services.Environment.Interfaces;

/// <summary>
/// The regex service.
/// </summary>
public interface IRegexService
{
    /// <summary>
    /// Validates the regex.
    /// </summary>
    /// <param name="regex">The regex.</param>
    /// <returns>A bool.</returns>
    bool ValidateRegex(string regex);

    /// <summary>
    /// Checks the if matches.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="pattern">The pattern.</param>
    /// <param name="options">The options.</param>
    /// <returns>A bool.</returns>
    bool CheckIfMatches(string input, string pattern, RegexOptions options);

    /// <summary>
    /// Gets the matches.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="pattern">The pattern.</param>
    /// <param name="options">The options.</param>
    /// <returns>A list of Matches.</returns>
    IList<Match> GetMatches(string input, string pattern, RegexOptions options);

    /// <summary>
    /// Replaces the.
    /// </summary>
    /// <param name="input">The input.</param>
    /// <param name="pattern">The pattern.</param>
    /// <param name="replacement">The replacement.</param>
    /// <param name="options">The options.</param>
    /// <returns>A string.</returns>
    string Replace(string input, string pattern, string replacement, RegexOptions options);
}