using Mapster;
using rendezvousBistro.Application.Authentication.Commands.Register;
using rendezvousBistro.Application.Authentication.Common;
using rendezvousBistro.Application.Authentication.Queries.Login;
using rendezvousBistro.Contracts.Authentication;

namespace rendezvousBistro.Api.Common.Mapping;

/// <summary>
/// Authentication mapping configuration
/// </summary> <summary>
/// 
/// </summary>
public class AuthenticationMappingConfig : IRegister
{
    /// <summary>
    /// Register authentication mapping profiles
    /// </summary>
    /// <param name="config"></param>
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User);
    }
}