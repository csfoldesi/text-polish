namespace Application.Common.Interfaces;

public interface IAIService
{
    Task<string> TestAsync();
    Task<AIResult<string>> FixTextAsync(string text);
}
