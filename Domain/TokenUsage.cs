namespace Domain;

public class TokenUsage : BaseEntity
{
    public required string Model { get; set; }
    public int InputTokenCount { get; set; }
    public int OutputTokenCount { get; set; }

    public required string UserId { get; set; }
    public User? User { get; set; }
}
