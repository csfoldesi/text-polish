namespace Application.Common;

public class AIResult<T>
{
    public T? Result { get; set; }
    public string? Model { get; set; }
    public int InputTokenCount { get; set; }
    public int OutputTokenCount { get; set; }
}
