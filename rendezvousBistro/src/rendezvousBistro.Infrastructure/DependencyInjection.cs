using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using rendezvousBistro.Application.Common.Interfaces.Authentication;
using rendezvousBistro.Application.Common.Interfaces.Persistence;
using rendezvousBistro.Application.Common.Interfaces.Services;
using rendezvousBistro.Infrastructure.Authentication;
using rendezvousBistro.Infrastructure.Persistence;
using rendezvousBistro.Infrastructure.Services;

namespace rendezvousBistro.Infrastructure;

/// <summary>
/// Dependency injection for infrastructure layer
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Register infrastructure layer dependencies
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services
            .AddAuth(configuration)
            .AddPersistence();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }

    /// <summary>
    /// Register repositories
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddPersistence(
        this IServiceCollection services
    )
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        return services;
    }

    /// <summary>
    /// Register authentication
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // extract the JwtSettings from the configuration and add it to the services
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });
        return services;
    }
}
