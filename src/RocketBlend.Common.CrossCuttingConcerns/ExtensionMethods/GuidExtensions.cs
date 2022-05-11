namespace RocketBlend.Common.CrossCuttingConcerns.ExtensionMethods;

/// <summary>
/// The guid extensions.
/// </summary>
public static class GuidExtensions
{
    /// <summary>
    /// Are the null or empty.
    /// </summary>
    /// <param name="guid">The guid.</param>
    /// <returns>A bool.</returns>
    public static bool IsNullOrEmpty(this Guid? guid)
    {
        return guid == null || guid == Guid.Empty;
    }
}
