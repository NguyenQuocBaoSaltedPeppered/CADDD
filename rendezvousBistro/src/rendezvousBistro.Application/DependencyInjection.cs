using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using rendezvousBistro.Application.Common.Behaviors;

namespace rendezvousBistro.Application;

/// <summary>
/// Dependency injection for application layer
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Register application layer dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config => config
            .RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}