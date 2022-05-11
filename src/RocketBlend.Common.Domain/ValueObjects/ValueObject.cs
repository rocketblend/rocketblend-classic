using System.Reflection;

namespace RocketBlend.Common.Domain.ValueObjects;

/// <summary>
/// Value object abstract.
/// Source: https://github.com/jhewlett/ValueObject///
/// </summary>
public abstract class ValueObject : IEquatable<ValueObject>
{
    private List<PropertyInfo>? properties;
    private List<FieldInfo>? fields;

    public static bool operator ==(ValueObject? obj1, ValueObject? obj2)
    {
        return Equals(obj1, null) ? Equals(obj2, null) : obj1.Equals(obj2);
    }

    public static bool operator !=(ValueObject? obj1, ValueObject? obj2)
    {
        return !(obj1 == obj2);
    }

    /// <summary>
    /// Equals other object.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>A bool.</returns>
    public bool Equals(ValueObject? obj)
    {
        return this.Equals(obj as object);
    }

    /// <summary>
    /// Equals other object.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <returns>A bool.</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || this.GetType() != obj.GetType()) return false;

        return this.GetProperties().All(p => this.PropertiesAreEqual(obj, p))
            && this.GetFields().All(f => this.FieldsAreEqual(obj, f));
    }

    /// <summary>
    /// Properties the are equal.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <param name="p">The p.</param>
    /// <returns>A bool.</returns>
    private bool PropertiesAreEqual(object obj, PropertyInfo p)
    {
        return Equals(p.GetValue(this, null), p.GetValue(obj, null));
    }

    /// <summary>
    /// Fields the are equal.
    /// </summary>
    /// <param name="obj">The obj.</param>
    /// <param name="f">The f.</param>
    /// <returns>A bool.</returns>
    private bool FieldsAreEqual(object obj, FieldInfo f)
    {
        return Equals(f.GetValue(this), f.GetValue(obj));
    }

    /// <summary>
    /// Gets the properties.
    /// </summary>
    /// <returns>A list of PropertyInfos.</returns>
    private IEnumerable<PropertyInfo> GetProperties()
    {
        if (this.properties == null)
        {
            this.properties = this.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                .ToList();

            // Not available in Core
            // !Attribute.IsDefined(p, typeof(IgnoreMemberAttribute))).ToList();
        }

        return this.properties;
    }

    /// <summary>
    /// Gets the fields.
    /// </summary>
    /// <returns>A list of FieldInfos.</returns>
    private IEnumerable<FieldInfo> GetFields()
    {
        if (this.fields == null)
        {
            this.fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                .ToList();
        }

        return this.fields;
    }

    /// <summary>
    /// Gets the hash code.
    /// </summary>
    /// <returns>An int.</returns>
    public override int GetHashCode()
    {
        unchecked   //allow overflow
        {
            int hash = 17;
            foreach (var prop in this.GetProperties())
            {
                var value = prop.GetValue(this, null);
                hash = this.HashValue(hash, value);
            }

            foreach (var field in this.GetFields())
            {
                var value = field.GetValue(this);
                hash = this.HashValue(hash, value);
            }

            return hash;
        }
    }

    /// <summary>
    /// Hashes the value.
    /// </summary>
    /// <param name="seed">The seed.</param>
    /// <param name="value">The value.</param>
    /// <returns>An int.</returns>
    private int HashValue(int seed, object? value)
    {
        var currentHash = value != null
            ? value.GetHashCode()
            : 0;

        return seed * 23 + currentHash;
    }
}