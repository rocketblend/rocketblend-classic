using System.Text;

namespace RocketBlend.Common.CrossCuttingConcerns.ExtensionMethods;

/// <summary>
/// The type extensions.
/// </summary>
public static class TypeExtensions
{
    /// <summary>
    /// Have the interface.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="interfaceType">The interface type.</param>
    /// <returns>A bool.</returns>
    public static bool HasInterface(this Type type, Type interfaceType)
    {
        return type.GetInterfacesOf(interfaceType).Length > 0;
    }

    /// <summary>
    /// Gets the interfaces of.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="interfaceType">The interface type.</param>
    /// <returns>An array of Types.</returns>
    public static Type[] GetInterfacesOf(this Type type, Type interfaceType)
    {
        return type.FindInterfaces((i, _) => i.GetGenericTypeDefinitionSafe() == interfaceType, null);
    }

    /// <summary>
    /// Gets the generic type definition safe.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>A Type.</returns>
    public static Type GetGenericTypeDefinitionSafe(this Type type)
    {
        return type.IsGenericType
            ? type.GetGenericTypeDefinition()
            : type;
    }

    /// <summary>
    /// Makes the generic type safe.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="typeArguments">The type arguments.</param>
    /// <returns>A Type.</returns>
    public static Type MakeGenericTypeSafe(this Type type, params Type[] typeArguments)
    {
        return type.IsGenericType && !type.GenericTypeArguments.Any()
            ? type.MakeGenericType(typeArguments)
            : type;
    }

    /// <summary>
    /// Generates the mapping code.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>A string.</returns>
    public static string GenerateMappingCode(this Type type)
    {
        var names = type.GetProperties().Select(x => x.Name);

        var text1 = new StringBuilder();
        var text2 = new StringBuilder();
        var text3 = new StringBuilder();
        var text4 = new StringBuilder();

        foreach (var name in names)
        {
            text1.Append($"a.{name} = {name};{Environment.NewLine}");
            text2.Append($"{name} = b.{name};{Environment.NewLine}");
            text3.Append($"{name} = b.{name},{Environment.NewLine}");
            text4.Append($"a.{name} = b.{name};{Environment.NewLine}");
        }

        return text1.ToString()
            + "--------------------------------------" + Environment.NewLine
            + text2.ToString()
            + "--------------------------------------" + Environment.NewLine
            + text3.ToString()
            + "--------------------------------------" + Environment.NewLine
            + text4.ToString();
    }
}
