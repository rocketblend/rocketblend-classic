namespace RocketBlend.Common.Domain.ValueObjects;

/// <summary>
/// The ignore member attribute.
/// Source: https://github.com/jhewlett/ValueObject
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]

public class IgnoreMemberAttribute : Attribute
{
}
