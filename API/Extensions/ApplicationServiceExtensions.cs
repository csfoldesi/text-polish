namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddAPIServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        //services.AddScoped<IUser, CurrentUser>();
        services.AddOpenApi();

        services.AddHttpContextAccessor();
        //services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddCors(options =>
        {
            options.AddPolicy(
                "CorsPolicy",
                policy =>
                {
                    policy
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithOrigins(
                            [.. configuration.GetSection("AllowedOrigins")!.Get<List<string>>()!]
                        );
                }
            );
        });

        return services;
    }
}
