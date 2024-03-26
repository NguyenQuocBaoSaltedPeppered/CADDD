using System.Reflection;
using Mapster;
using MapsterMapper;

namespace rendezvousBistro.Api.Common.Mapping;

/// <summary>
/// Dependency injection for mapping
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Register mapping dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        return services;
    }
}