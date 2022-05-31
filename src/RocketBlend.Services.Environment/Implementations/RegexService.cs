using System.Text.RegularExpressions;
using RocketBlend.Services.Environment.Interfaces;

namespace RocketBlend.Services.Environment.Implementations;

/// <summary>
/// The regex service.
/// </summary>
public class RegexService : IRegexService
{
    /// <inheritdoc />
    public bool ValidateRegex(string regex)
    {
        if (string.IsNullOrEmpty(regex))
        {
            return false;
        }

        try
        {
            _ = new Regex(regex);
        }
        catch (ArgumentException)
        {
            return false;
        }

        return true;
    }

    /// <inheritdoc />
    public bool CheckIfMatches(string input, string pattern, RegexOptions options) =>
        Regex.IsMatch(input, pattern, options);

    /// <inheritdoc />
    public IList<Match> GetMatches(string input, string pattern, RegexOptions options) =>
        Regex.Matches(input, pattern, options);

    /// <inheritdoc />
    public string Replace(string input, string pattern, string replacement, RegexOptions options) =>
        Regex.Replace(input, pattern, replacement, options);
}