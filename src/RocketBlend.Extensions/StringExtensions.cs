namespace RocketBlend.Extensions;

/// <summary>
/// The string extensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// To the title case.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>A string.</returns>
    public static string ToTitleCase(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return value;
        }

        if (value.Length < 2)
        {
            return value.ToUpper();
        }

        return char.ToUpper(value[0]) + value[1..];
    }
}