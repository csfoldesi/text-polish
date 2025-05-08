using Application.Common.Interfaces;
using OpenAI.Chat;

namespace Infrastructure.OpenAI;

public class OpenAIService : IAIService
{
    private readonly ChatClient _client;

    public OpenAIService(ChatClient client)
    {
        _client = client;
    }

    public async Task<string> TestAsync()
    {
        ChatCompletion completion = await _client.CompleteChatAsync("Say 'this is a test.'");
        return completion.Content[0].Text;
    }
}
