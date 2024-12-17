using Microsoft.Extensions.DependencyInjection;
using TrainerJournal_backend.Application.Services;
using TrainerJournal_backend.Application.Services.Jwt;
using TrainerJournal_backend.Application.Services.Students;
using TrainerJournal_backend.Application.Services.Trainers;

namespace TrainerJournal_backend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddTransient<JwtGenerator>();
        services.AddTransient<AuthService>();
        services.AddTransient<TrainerService>();
        services.AddTransient<StudentsService>();
        
        return services;
    }
}