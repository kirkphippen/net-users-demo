namespace NetUsersApi.Models;

/// <summary>
/// Represents a standardized error response for the API
/// </summary>
public class ApiError
{
    /// <summary>
    /// Error code identifying the type of error
    /// </summary>
    public required string ErrorCode { get; set; }

    /// <summary>
    /// Human-readable error message
    /// </summary>
    public required string Message { get; set; }

    /// <summary>
    /// Optional additional details about the error
    /// </summary>
    public object? Details { get; set; }

    /// <summary>
    /// Timestamp when the error occurred
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Request path where the error occurred
    /// </summary>
    public required string Path { get; set; }
}