namespace NetUsersApi.Models;

/// <summary>
/// Represents user profile data
/// </summary>
public class UserProfile
{
    public required string Id { get; set; }
    public required string FullName { get; set; }
    public required string Emoji { get; set; }
}
