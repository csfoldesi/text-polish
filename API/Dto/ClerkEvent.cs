using System.Text.Json.Serialization;

namespace API.Dto;

public class ClerkEvent
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("data")]
    public ClerkUserData? Data { get; set; }
}

public class ClerkUserData
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("email_addresses")]
    public List<ClerkEmailAddress> EmailAddresses { get; set; } = [];
}

public class ClerkEmailAddress
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("email_address")]
    public required string EmailAddress { get; set; }
}
