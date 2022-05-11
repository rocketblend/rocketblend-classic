using System.Reflection;
using AutoMapper;

namespace RocketBlend.Common.Application.Mappings;

/// <summary>
/// The mapping profile.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile"/> class.
    /// </summary>
    public MappingProfile()
    {
        var assemblyReferences = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic)
            .ToList();
        this.ApplyMappingsFromAssembly(assemblyReferences);
    }

    /// <inheritdoc />
    private void ApplyMappingsFromAssembly(List<Assembly> assemblies)
    {
        foreach (Assembly assembly in assemblies)
        {
            var assemblyName = assembly.GetName().Name;

            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1")!.GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });

            }
        }
    }
}
