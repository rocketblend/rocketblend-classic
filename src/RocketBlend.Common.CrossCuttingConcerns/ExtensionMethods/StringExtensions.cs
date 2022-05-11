using System.Text;

namespace RocketBlend.Common.CrossCuttingConcerns.ExtensionMethods;

/// <summary>
/// The string extensions.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Lefts the.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="length">The length.</param>
    /// <returns>A string.</returns>
    public static string Left(this string value, int length)
    {
        length = Math.Abs(length);
        return string.IsNullOrEmpty(value) ? value : value[..Math.Min(value.Length, length)];
    }

    /// <summary>
    /// Rights the.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="length">The length.</param>
    /// <returns>A string.</returns>
    public static string Right(this string value, int length)
    {
        length = Math.Abs(length);
        return string.IsNullOrEmpty(value) ? value : value[^(Math.Min(value.Length, length))..];
    }

    /// <summary>
    /// Ins the.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="list">The list.</param>
    /// <returns>A bool.</returns>
    public static bool In(this string value, List<string> list)
    {
        return list.Contains(value, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Nots the in.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="list">The list.</param>
    /// <returns>A bool.</returns>
    public static bool NotIn(this string value, List<string> list)
    {
        return !In(value, list);
    }

    /// <summary>
    /// Equals the ignore case.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="toCheck">The to check.</param>
    /// <returns>A bool.</returns>
    public static bool EqualsIgnoreCase(this string source, string toCheck)
    {
        return string.Equals(source, toCheck, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Tos the base64.
    /// </summary>
    /// <param name="src">The src.</param>
    /// <returns>A string.</returns>
    public static string ToBase64(this string src)
    {
        byte[] b = Encoding.UTF8.GetBytes(src);
        return Convert.ToBase64String(b);
    }

    /// <summary>
    /// Tos the base64.
    /// </summary>
    /// <param name="src">The src.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>A string.</returns>
    public static string ToBase64(this string src, Encoding encoding)
    {
        byte[] b = encoding.GetBytes(src);
        return Convert.ToBase64String(b);
    }

    /// <summary>
    /// Froms the base64 string.
    /// </summary>
    /// <param name="src">The src.</param>
    /// <returns>A string.</returns>
    public static string FromBase64String(this string src)
    {
        byte[] b = Convert.FromBase64String(src);
        return Encoding.UTF8.GetString(b);
    }

    /// <summary>
    /// Froms the base64 string.
    /// </summary>
    /// <param name="src">The src.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>A string.</returns>
    public static string FromBase64String(this string src, Encoding encoding)
    {
        byte[] b = Convert.FromBase64String(src);
        return encoding.GetString(b);
    }

    /// <summary>
    /// Removes the.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="removedValues">The removed values.</param>
    /// <returns>A string.</returns>
    public static string Remove(this string source, params string[] removedValues)
    {
        removedValues.ToList().ForEach(x => source = source.Replace(x, ""));
        return source;
    }
}
