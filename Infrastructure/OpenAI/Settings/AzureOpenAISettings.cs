namespace Infrastructure.OpenAI.Settings;

public class AzureOpenAISettings
{
    public required string Endpoint { get; set; }
    public required string ApiKey { get; set; }
    public required string ApiVersion { get; set; }
    public required string Model { get; set; }
}
