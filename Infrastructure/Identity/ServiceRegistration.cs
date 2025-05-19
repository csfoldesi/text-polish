using Application.Common.Interfaces;
using Domain;
using Infrastructure.Identity.Settings;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Identity;

public static class ServiceRegistration
{
    public static IServiceCollection AddIdentityServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<IIdentityService, IdentityService>();

        var clerkConfig = configuration.GetSection(nameof(ClerkSettings)).Get<ClerkSettings>()!;

        services
            .AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DataContext>()
            .AddSignInManager<SignInManager<User>>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
                "Clerk",
                options =>
                {
                    options.Authority = clerkConfig.Authority;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = clerkConfig.Issuer,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                    };
                }
            );

        services.AddAuthorization(options =>
        {
            var policy = new AuthorizationPolicyBuilder("Clerk").RequireAuthenticatedUser().Build();
            options.DefaultPolicy = policy;
        });

        return services;
    }
}
