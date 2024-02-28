using Microsoft.AspNetCore.Mvc.Infrastructure;
using rendezvousBistro.Api.Common.Errors;
using rendezvousBistro.Api.Common.Mapping;

namespace rendezvousBistro.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMapping();
        services.AddControllers();

        // Add custom problem details factory
        services.AddSingleton<ProblemDetailsFactory, RendezvousBistroProblemDetailFactory>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}