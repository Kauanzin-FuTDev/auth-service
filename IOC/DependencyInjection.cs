using Application.Interfaces;
using Domain.Repository;
using Infraestructer.Context;
using Infraestructer.Persistence.Repository;
using Infraestructer.Services.AuthService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IOC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,  IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres");
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString ?? throw new InvalidOperationException("Connection string 'Postgres' not found.")));
        
        // Injeção do serviços
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        

        return services;
    }

}