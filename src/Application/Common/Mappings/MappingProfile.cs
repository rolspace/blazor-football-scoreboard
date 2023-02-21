using System.Reflection;
using AutoMapper;

namespace Football.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(MapFrom<>);

        var mappingMethodName = nameof(MapFrom<object>.Mapping);

        var types = assembly.GetExportedTypes().Where(t => t.BaseType != null
            && t.BaseType.IsGenericType
            && t.BaseType.GetGenericTypeDefinition() == mapFromType).ToList();

        var argumentTypes = new Type[] { typeof(Profile) };

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            methodInfo.Invoke(instance, new object[] { this });

        }
    }
}
