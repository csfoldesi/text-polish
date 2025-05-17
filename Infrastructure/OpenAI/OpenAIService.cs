using Application.Common;
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

    public async Task<AIResult<string>> FixTextAsync(string text)
    {
        var prompt =
            $@"Please review the following text for grammar errors, spelling mistakes, and typos.
Then, rewrite it to sound more natural and professional while preserving the original meaning.
Provide only the improved version.
Text: {text}";
        ChatCompletion completion = await _client.CompleteChatAsync(prompt);
        return new AIResult<string>
        {
            Result = completion.Content[0].Text,
            Model = completion.Model,
            InputTokenCount = completion.Usage.InputTokenCount,
            OutputTokenCount = completion.Usage.OutputTokenCount,
        };
    }

    public async Task<string> TestAsync()
    {
        ChatCompletion completion = await _client.CompleteChatAsync("Say 'this is a test.'");
        return completion.Content[0].Text;
    }
}
