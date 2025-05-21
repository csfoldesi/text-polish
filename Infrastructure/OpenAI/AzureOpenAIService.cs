using Application.Common;
using Application.Common.Interfaces;
using OpenAI.Chat;

namespace Infrastructure.OpenAI;

public class AzureOpenAIService : IAIService
{
    private readonly ChatClient _client;

    public AzureOpenAIService(ChatClient client)
    {
        _client = client;
    }

    public async Task<AIResult<string>> FixTextAsync(string text)
    {
        var requestOptions = new ChatCompletionOptions()
        {
            MaxOutputTokenCount = 800,
            Temperature = 1.0f,
            TopP = 1.0f,
            FrequencyPenalty = 0.0f,
            PresencePenalty = 0.0f,
        };
        var prompt =
            $@"Please review the following text for grammar errors, spelling mistakes, and typos.
Then, rewrite it to sound more natural and professional while preserving the original meaning.
Provide only the improved version.
Use English as output language.";
        List<ChatMessage> messages = [new SystemChatMessage(prompt), new UserChatMessage(text)];

        ChatCompletion completion = await _client.CompleteChatAsync(messages, requestOptions);
        return new AIResult<string>
        {
            Result = completion.Content[0].Text,
            Model = completion.Model,
            InputTokenCount = completion.Usage.InputTokenCount,
            OutputTokenCount = completion.Usage.OutputTokenCount,
        };
    }

    public Task<string> TestAsync()
    {
        throw new NotImplementedException();
    }
}
