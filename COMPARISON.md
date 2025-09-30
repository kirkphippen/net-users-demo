# Go vs .NET Implementation Comparison

This document shows how the Go implementation maps to the .NET implementation.

## Project Structure Comparison

### Go (go-users-demo)
```
go-users-demo/
├── main.go                      # Entry point
├── api/
│   └── routes.go               # Route configuration
├── controllers/
│   └── user_controller.go      # Request handlers
├── models/
│   └── user.go                 # Data model
└── templates/
    └── users.html              # HTML template
```

### .NET (net-users-api)
```
net-users-api/
├── Program.cs                  # Entry point + route configuration
├── Controllers/
│   ├── HomeController.cs      # Root endpoint handler
│   └── UsersController.cs     # API request handlers
├── Models/
│   └── UserProfile.cs         # Data model
└── Views/
    └── Home/
        └── Index.cshtml       # Razor template
```

## Code Mapping

### 1. Data Model

**Go (`models/user.go`):**
```go
type UserProfile struct {
    ID       string `json:"id"`
    FullName string `json:"fullName"`
    Emoji    string `json:"emoji"`
}
```

**C# (`Models/UserProfile.cs`):**
```csharp
public class UserProfile
{
    public required string Id { get; set; }
    public required string FullName { get; set; }
    public required string Emoji { get; set; }
}
```

### 2. Sample Data

**Go:**
```go
var users = []models.UserProfile{
    {ID: "1", FullName: "John Doe", Emoji: "😀"},
    {ID: "2", FullName: "Jane Smith", Emoji: "🚀"},
    {ID: "3", FullName: "Robert Johnson", Emoji: "🎸"},
}
```

**C#:**
```csharp
private static List<UserProfile> _users = new()
{
    new UserProfile { Id = "1", FullName = "John Doe", Emoji = "😀" },
    new UserProfile { Id = "2", FullName = "Jane Smith", Emoji = "🚀" },
    new UserProfile { Id = "3", FullName = "Robert Johnson", Emoji = "🎸" }
};
```

### 3. Get All Users

**Go:**
```go
func GetUsers(c *gin.Context) {
    log.Println("GET /api/v1/users endpoint called")
    c.JSON(http.StatusOK, users)
}
```

**C#:**
```csharp
[HttpGet]
public ActionResult<IEnumerable<UserProfile>> GetUsers()
{
    _logger.LogInformation("GET /api/v1/users endpoint called");
    return Ok(_users);
}
```

### 4. Get User by ID

**Go:**
```go
func GetUser(c *gin.Context) {
    id := c.Param("id")
    
    for _, user := range users {
        if user.ID == id {
            c.JSON(http.StatusOK, user)
            return
        }
    }
    
    c.JSON(http.StatusNotFound, gin.H{"error": "User not found"})
}
```

**C#:**
```csharp
[HttpGet("{id}")]
public ActionResult<UserProfile> GetUser(string id)
{
    var user = _users.FirstOrDefault(u => u.Id == id);
    
    if (user == null)
    {
        return NotFound(new { error = "User not found" });
    }
    
    return Ok(user);
}
```

### 5. Create User

**Go:**
```go
func CreateUser(c *gin.Context) {
    var newUser models.UserProfile
    
    if err := c.ShouldBindJSON(&newUser); err != nil {
        c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
        return
    }
    
    users = append(users, newUser)
    c.JSON(http.StatusCreated, newUser)
}
```

**C#:**
```csharp
[HttpPost]
public ActionResult<UserProfile> CreateUser([FromBody] UserProfile newUser)
{
    if (newUser == null)
    {
        return BadRequest(new { error = "Invalid user data" });
    }

    _users.Add(newUser);
    return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
}
```

### 6. Update User

**Go:**
```go
func UpdateUser(c *gin.Context) {
    id := c.Param("id")
    var updatedUser models.UserProfile
    
    if err := c.ShouldBindJSON(&updatedUser); err != nil {
        c.JSON(http.StatusBadRequest, gin.H{"error": err.Error()})
        return
    }
    
    for i, user := range users {
        if user.ID == id {
            updatedUser.ID = id
            users[i] = updatedUser
            c.JSON(http.StatusOK, updatedUser)
            return
        }
    }
    
    c.JSON(http.StatusNotFound, gin.H{"error": "User not found"})
}
```

**C#:**
```csharp
[HttpPut("{id}")]
public ActionResult<UserProfile> UpdateUser(string id, [FromBody] UserProfile updatedUser)
{
    if (updatedUser == null)
    {
        return BadRequest(new { error = "Invalid user data" });
    }

    var index = _users.FindIndex(u => u.Id == id);
    
    if (index == -1)
    {
        return NotFound(new { error = "User not found" });
    }
    
    updatedUser.Id = id;
    _users[index] = updatedUser;
    return Ok(updatedUser);
}
```

### 7. Delete User (TODO)

**Go:**
```go
// DeleteUser removes a user by ID TODO
func DeleteUser(c *gin.Context) {
    // Implement delete functionality here using Copilot Ask or Edit mode
}
```

**C#:**
```csharp
/// <summary>
/// Delete a user by ID TODO
/// </summary>
[HttpDelete("{id}")]
public IActionResult DeleteUser(string id)
{
    // Implement delete functionality here using Copilot Ask or Edit mode
    throw new NotImplementedException("DeleteUser functionality not yet implemented");
}
```

### 8. Root Endpoint (HTML View)

**Go (`controllers/user_controller.go`):**
```go
func HomePageHandler(c *gin.Context) {
    log.Println("GET / endpoint called")
    c.HTML(http.StatusOK, "users.html", gin.H{
        "Users": users,
    })
}
```

**C# (`Controllers/HomeController.cs`):**
```csharp
public IActionResult Index()
{
    _logger.LogInformation("GET / endpoint called");
    var users = UsersController.GetAllUsers();
    return View(users);
}
```

### 9. Route Configuration

**Go (`api/routes.go`):**
```go
func SetupRouter() *gin.Engine {
    router := gin.Default()
    router.LoadHTMLGlob(templatesPath)
    
    router.GET("/", controllers.HomePageHandler)
    
    v1 := router.Group("/api/v1")
    {
        users := v1.Group("/users")
        {
            users.GET("", controllers.GetUsers)
            users.GET("/:id", controllers.GetUser)
            users.POST("", controllers.CreateUser)
            users.PUT("/:id", controllers.UpdateUser)
        }
    }
    
    return router
}
```

**C# (`Program.cs`):**
```csharp
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

// ... 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();
```

### 10. Entry Point

**Go (`main.go`):**
```go
func main() {
    router := api.SetupRouter()
    log.Println("Starting server on :8080")
    router.Run(":8080")
}
```

**C# (`Program.cs`):**
```csharp
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(8080);
});

var app = builder.Build();

Console.WriteLine("Starting server on :8080");
app.Run();
```

## Key Differences

### Language Features

| Aspect | Go | C# |
|--------|----|----|
| Error Handling | Explicit error returns | Exceptions |
| Null Safety | Explicit pointer checks | Nullable reference types |
| Collections | Slices | Generic Lists |
| Web Framework | Gin | ASP.NET Core |
| Template Engine | Go templates | Razor |
| Dependency Injection | Manual | Built-in |
| Routing | Manual configuration | Attribute-based + convention |

### Framework Conventions

- **Go/Gin**: Explicit route registration
- **C#/ASP.NET Core**: Attribute-based routing with conventions

### Testing

- **Go**: `go test` with `testing` package
- **C#**: `dotnet test` with xUnit/NUnit/MSTest

## Similarities

✅ Both use in-memory storage
✅ Both have the same endpoints
✅ Both return JSON for API calls
✅ Both have HTML view at root
✅ Both run on port 8080
✅ Both have DeleteUser as TODO
✅ Both include Copilot practice instructions
