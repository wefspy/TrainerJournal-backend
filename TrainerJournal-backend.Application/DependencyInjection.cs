using Microsoft.Extensions.DependencyInjection;
using TrainerJournal_backend.Application.Services;
using TrainerJournal_backend.Application.Services.Jwt;

namespace TrainerJournal_backend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddTransient<JwtGenerator>();
        services.AddTransient<AuthService>();
        
        return services;
    }
}