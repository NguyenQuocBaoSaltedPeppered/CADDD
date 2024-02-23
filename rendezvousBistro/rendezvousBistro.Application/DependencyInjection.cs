using Microsoft.Extensions.DependencyInjection;
using rendezvousBistro.Application.Services.Authentication;

namespace rendezvousBistro.Application;

public static class DependencyInjection
{
    // Thực ra, bên Api hoàn toàn có thể gọi đến các service của Application tại
    // Program.cs của Api. Tuy nhiên trong clean Architecture, các layer nên tự
    // đăng ký các dịch vụ của nó ra ngoài và Program.cs tại Api chỉ việc gọi tới
    // 1 method để có thể truy cập được các dịch vụ được cung cấp tại phía Application.
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}