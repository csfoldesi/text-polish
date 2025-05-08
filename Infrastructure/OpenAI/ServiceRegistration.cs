using Application.Common.Interfaces;
using Infrastructure.OpenAI.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Chat;

namespace Infrastructure.OpenAI;

public static class ServiceRegistration
{
    public static IServiceCollection AddOpenAIServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var config = configuration.GetSection(nameof(OpenAISettings)).Get<OpenAISettings>()!;
        services.AddSingleton(new ChatClient(model: "gpt-4o", apiKey: config!.ApiKey));

        services.AddScoped<IAIService, OpenAIService>();
        return services;
    }
}
