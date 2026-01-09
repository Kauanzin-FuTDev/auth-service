using Application.Interfaces;
using Application.services;
using Infraestructer.Persistence;
using Infraestructer.Services.AuthService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IOC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,  IConfiguration configuration)
    {
        var mongoSettings = configuration.GetSection("MongoSettings").Get<MongoSettings>();
        services.AddSingleton(mongoSettings);
        
        
        // Injeção do serviço de Token
        services.AddScoped<ITokenService, TokenService>();
        

        return services;
    }

}