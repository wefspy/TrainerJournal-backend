using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TrainerJournal_backend.Application.Options;
using TrainerJournal_backend.Domain.Entities;
using TrainerJournal_backend.Infrastructure;

namespace TrainerJournal_backend.API;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfigurationSection jwtSection)
    {
        var jwtOptions = new JwtOptions();
        jwtSection.Bind(jwtOptions);

        services.Configure<JwtOptions>(options =>
        {
            options.Audience = jwtOptions.Audience;
            options.Issuer = jwtOptions.Issuer;
            options.Secret = jwtOptions.Secret;
            options.ExpiryMinutes = jwtOptions.ExpiryMinutes;
        });

        services.AddIdentity<UserIdentity, RoleIdentity>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = jwtOptions.GetSymmetricSecurityKey(),
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }

    public static void AddCustomCors(this IServiceCollection services, string[]? origins)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("FrontendPolicy", policy =>
            {
                if (origins != null)
                    policy.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
                else
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            });
        });
    }
    
    public static IServiceCollection AddSwaggerWithJwtSecurity(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Trainer Journal API", Version = "v1" });

            // Configuring Swagger to Send a JWT Token in a Request
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "Authorization",
                Description = "Put JWT Bearer token on textbox below!",
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        return services;
    }
}