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

                    // Map Clerk claims to ASP.NET Core claims
                    /*options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var claimsIdentity = context.Principal?.Identity as ClaimsIdentity;

                            // Extract Clerk user ID
                            var userIdClaim = claimsIdentity?.FindFirst("sub");
                            if (userIdClaim != null)
                            {
                                claimsIdentity!.AddClaim(
                                    new Claim(ClaimTypes.NameIdentifier, userIdClaim.Value)
                                );
                            }
                            var userId = claimsIdentity!
                                .FindFirst(ClaimTypes.NameIdentifier)
                                ?.Value;
                            var email = claimsIdentity!.FindFirst(ClaimTypes.Email)?.Value;
                            var name = string.Format(
                                "{0} {1}",
                                claimsIdentity!.FindFirst(ClaimTypes.GivenName)?.Value,
                                claimsIdentity!.FindFirst(ClaimTypes.Surname)?.Value
                            );
                            var role = claimsIdentity!.FindFirst(ClaimTypes.Role)?.Value;
                            userId = await CreateOauthUserAsync(context, userId, email, name, role);
                            claimsIdentity!.AddClaim(new Claim("global_id", userId));
                        },*/
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
