using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rendezvousBistro.Application.Common.Interfaces.Authentication;
using rendezvousBistro.Application.Common.Interfaces.Persistence;
using rendezvousBistro.Application.Common.Interfaces.Services;
using rendezvousBistro.Infrastructure.Authentication;
using rendezvousBistro.Infrastructure.Persistence;
using rendezvousBistro.Infrastructure.Services;

namespace rendezvousBistro.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}
