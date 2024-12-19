using Microsoft.Extensions.DependencyInjection;
using TrainerJournal_backend.Application.Services;
using TrainerJournal_backend.Application.Services.Attendance;
using TrainerJournal_backend.Application.Services.Groups;
using TrainerJournal_backend.Application.Services.Jwt;
using TrainerJournal_backend.Application.Services.Payments;
using TrainerJournal_backend.Application.Services.Practices;
using TrainerJournal_backend.Application.Services.Students;
using TrainerJournal_backend.Application.Services.Trainers;
using TrainerJournal_backend.Application.Services.Wallets;
using TrainerJournal_backend.Domain.Entities;

namespace TrainerJournal_backend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddTransient<JwtGenerator>();
        services.AddTransient<AuthService>();
        services.AddTransient<TrainerService>();
        services.AddTransient<StudentsService>();
        services.AddTransient<GroupsService>();
        services.AddTransient<PracticesService>();
        services.AddTransient<AttendanceService>();
        services.AddTransient<WalletsService>();
        services.AddTransient<PaymentsService>();
        
        return services;
    }
}