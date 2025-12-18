using Application.Interfaces;
using Infraestructer.Services.AuthService;
using Microsoft.Extensions.DependencyInjection;

namespace IOC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Injeção do serviço de Token
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }

}