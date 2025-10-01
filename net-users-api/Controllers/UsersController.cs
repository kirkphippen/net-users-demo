using Microsoft.AspNetCore.Mvc;
using NetUsersApi.Models;
using NetUsersApi.Exceptions;
using Microsoft.AspNetCore.JsonPatch;

namespace NetUsersApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;

    // Sample user data. Id, FullName, Emoji
    private static List<UserProfile> _users = new()
    {
        new UserProfile { Id = "1", FullName = "John Doe", Emoji = "😀" },
        new UserProfile { Id = "2", FullName = "Jane Smith", Emoji = "🚀" },
        new UserProfile { Id = "3", FullName = "Robert Johnson", Emoji = "🎸" },
        new UserProfile { Id = "4", FullName = "Mario", Emoji = "🍄" },
        new UserProfile { Id = "5", FullName = "Luigi", Emoji = "👨‍🌾" },
        new UserProfile { Id = "6", FullName = "Princess Peach", Emoji = "👸" }
    };

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>List of all user profiles</returns>
    [HttpGet]
    public ActionResult<IEnumerable<UserProfile>> GetUsers()
    {
        _logger.LogInformation("GET /api/v1/users endpoint called");
        return Ok(_users);
    }

    /// <summary>
    /// Get a specific user by ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>User profile or 404 if not found</returns>
    [HttpGet("{id}")]
    public ActionResult<UserProfile> GetUser(string id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        
        if (user == null)
        {
            throw new ResourceNotFoundException("User", id);
        }
        
        return Ok(user);
    }

    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="newUser">User profile data</param>
    /// <returns>Created user profile</returns>
    [HttpPost]
    public ActionResult<UserProfile> CreateUser([FromBody] UserProfile newUser)
    {
        if (newUser == null)
        {
            throw new ValidationException("User data is required");
        }

        // Check for duplicate ID
        if (_users.Any(u => u.Id == newUser.Id))
        {
            throw new BusinessRuleException("DUPLICATE_USER_ID", $"User with ID '{newUser.Id}' already exists");
        }

        // For simplicity, we're just appending to the list
        // In a real application, you would use a database
        _users.Add(newUser);
        
        return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
    }

    /// <summary>
    /// Update an existing user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="updatedUser">Updated user profile data</param>
    /// <returns>Updated user profile or 404 if not found</returns>
    [HttpPut("{id}")]
    public ActionResult<UserProfile> UpdateUser(string id, [FromBody] UserProfile updatedUser)
    {
        if (updatedUser == null)
        {
            throw new ValidationException("User data is required");
        }

        var index = _users.FindIndex(u => u.Id == id);
        
        if (index == -1)
        {
            throw new ResourceNotFoundException("User", id);
        }
        
        // Ensure ID doesn't change
        updatedUser.Id = id;
        _users[index] = updatedUser;
        
        return Ok(updatedUser);
    }

    /// <summary>
    /// Partially update an existing user
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="patchData">JSON object with optional FullName and/or Emoji</param>
    /// <returns>Updated user profile or 404/400</returns>
    [HttpPatch("{id}")]
    public ActionResult<UserProfile> PatchUser(string id, [FromBody] Dictionary<string, object>? patchData)
    {
        if (patchData == null || patchData.Count == 0)
        {
            throw new ValidationException("No data provided for update");
        }

        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            throw new ResourceNotFoundException("User", id);
        }

        // Only allow FullName and Emoji to be updated
        bool updated = false;
        if (patchData.ContainsKey("Id"))
        {
            throw new ValidationException("Id cannot be updated");
        }
        if (patchData.ContainsKey("FullName"))
        {
            var fullNameObj = patchData["FullName"];
            if (fullNameObj == null || string.IsNullOrWhiteSpace(fullNameObj.ToString()))
                throw new ValidationException("FullName cannot be empty");
            user.FullName = fullNameObj.ToString()!;
            updated = true;
        }
        if (patchData.ContainsKey("Emoji"))
        {
            var emojiObj = patchData["Emoji"];
            if (emojiObj == null || string.IsNullOrWhiteSpace(emojiObj.ToString()))
                throw new ValidationException("Emoji cannot be empty");
            user.Emoji = emojiObj.ToString()!;
            updated = true;
        }
        if (!updated)
        {
            throw new ValidationException("No valid fields provided for update");
        }

        return Ok(user);
    }

    /// <summary>
    /// Delete a user by ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>No content or 404 if not found</returns>
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(string id)
    {
        _logger.LogInformation("DELETE /api/v1/users/{UserId} endpoint called", id);

        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            _logger.LogWarning("User with id: {UserId} not found for deletion", id);
            throw new ResourceNotFoundException("User", id);
        }

        _users.Remove(user);
        _logger.LogInformation("User with id: {UserId} deleted successfully", id);
        return NoContent();
    }

    /// <summary>
    /// Get all users for internal use (used by Home controller)
    /// </summary>
    /// <returns>List of all user profiles</returns>
    internal static List<UserProfile> GetAllUsers()
    {
        return _users;
    }
}
