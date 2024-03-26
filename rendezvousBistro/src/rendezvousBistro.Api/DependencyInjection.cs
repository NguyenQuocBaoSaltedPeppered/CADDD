using System.Reflection;

using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;

using rendezvousBistro.Api.Common.Errors;
using rendezvousBistro.Api.Common.Mapping;

namespace rendezvousBistro.Api;

/// <summary>
/// Dependency injection for the presentation layer
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Register presentation layer dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMapping();
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // Add swagger
        services.AddSwaggerConfiguration();

        // Add custom problem details factory
        services.AddSingleton<ProblemDetailsFactory, RendezvousBistroProblemDetailFactory>();
        return services;
    }

    /// <summary>
    /// Register swagger
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options => {
            // Enable XML comment on swaggerUI
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
            $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            options.EnableAnnotations();

            options.SwaggerDoc("rendezvous-bistro", new OpenApiInfo
            {
                Version = "v1",
                Title = "Rendezvous Bistro API",
                Description = "API for rendezvousBistro web interface"
            });
            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        });

        return services;
    }
}