# Quick Start Guide

## What Was Created

This project is a complete .NET 9 REST API that mirrors the functionality of the Go Users Demo from https://github.com/frye/go-users-demo.

### Project Components

1. **net-users-api/** - Main ASP.NET Core Web API project
   - Controllers for handling HTTP requests
   - Models defining the UserProfile data structure
   - Razor views for HTML table display
   - Complete REST API with CRUD operations

2. **Key Files:**
   - `Program.cs` - Application configuration (configured for port 8080)
   - `Controllers/UsersController.cs` - API endpoints (GET, POST, PUT, DELETE)
   - `Controllers/HomeController.cs` - Root endpoint for HTML view
   - `Views/Home/Index.cshtml` - Beautiful HTML table display
   - `Models/UserProfile.cs` - Data model

3. **Practice Materials:**
   - `Copilot_Practice_Instructions.md` - GitHub Copilot exercises
   - `Copilot_TDD_Practice_Instructions.md` - TDD exercises

## Running the Application

### Quick Start
```bash
cd net-users-demo
dotnet run --project net-users-api
```

The application will start on http://localhost:8080

### Test It Out

1. **View HTML Table:**
   - Open browser: http://localhost:8080
   - You'll see a nice table with user profiles

2. **Test API Endpoints:**
   ```bash
   # Get all users
   curl http://localhost:8080/api/v1/users
   
   # Get specific user
   curl http://localhost:8080/api/v1/users/1
   
   # Create new user
   curl -X POST http://localhost:8080/api/v1/users \
     -H "Content-Type: application/json" \
     -d '{"id":"4", "fullName":"Alice Cooper", "emoji":"🎭"}'
   
   # Update user
   curl -X PUT http://localhost:8080/api/v1/users/1 \
     -H "Content-Type: application/json" \
     -d '{"id":"1", "fullName":"John Updated", "emoji":"🔥"}'
   ```

## What Mirrors the Go Demo

✅ **Same functionality:**
- GET all users
- GET user by ID
- POST create user
- PUT update user
- DELETE endpoint (intentionally left as TODO for practice)

✅ **Same sample data:**
- 3 initial users (John Doe, Jane Smith, Robert Johnson)
- Same emojis and data structure

✅ **Same port:**
- Both run on port 8080

✅ **Same root endpoint behavior:**
- Both display a nice HTML table at `/`
- Both have JSON API at `/api/v1/users`

✅ **Same practice exercises:**
- Copilot inline completion
- Ask/Edit mode
- TDD mode
- Testing mode

## What's Not Yet Implemented (By Design)

The `DeleteUser` method is intentionally left as a TODO with a NotImplementedException. This matches the Go demo where the delete functionality is meant to be implemented as a practice exercise using GitHub Copilot.

## Next Steps

1. Follow the practice instructions in `Copilot_Practice_Instructions.md`
2. Try the TDD exercises in `Copilot_TDD_Practice_Instructions.md`
3. Implement the DeleteUser functionality using GitHub Copilot
4. Add unit tests using xUnit

## Notes

- The application uses in-memory storage (a static List)
- No database is required
- Data is reset when the application restarts
- Perfect for learning and experimentation
