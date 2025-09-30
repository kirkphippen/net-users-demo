# .NET Users Demo

A .NET 9 REST API demonstration project that mirrors the functionality of the [frye/go-users-demo](https://github.com/frye/go-users-demo) repository. This project is designed for practicing GitHub Copilot features and Test-Driven Development with ASP.NET Core.

## Project Structure

```
net-users-demo/
├── net-users-api/              # ASP.NET Core Web API project
│   ├── Controllers/
│   │   ├── HomeController.cs   # Handles root endpoint with HTML view
│   │   └── UsersController.cs  # REST API endpoints for user management
│   ├── Models/
│   │   └── UserProfile.cs      # User profile data model
│   ├── Views/
│   │   └── Home/
│   │       └── Index.cshtml    # HTML view displaying users in a table
│   ├── Program.cs              # Application entry point and configuration
│   ├── README.md               # Project-specific documentation
│   ├── Copilot_Practice_Instructions.md
│   └── Copilot_TDD_Practice_Instructions.md
└── net-users-demo.sln          # Solution file
```

## Features

- **RESTful API** for user profile management
- **HTML table view** at root endpoint for user-friendly display
- **In-memory data storage** (simple list)
- **Structured for learning** - includes TODO functionality for practice
- **GitHub Copilot practice exercises** included

## API Endpoints

- `GET /` - Display users in an HTML table
- `GET /api/v1/users` - Get all users (JSON)
- `GET /api/v1/users/{id}` - Get a specific user by ID
- `POST /api/v1/users` - Create a new user
- `PUT /api/v1/users/{id}` - Update an existing user
- `DELETE /api/v1/users/{id}` - Delete a user (TODO: Not yet implemented)

## Data Model

Each user profile contains:
- `id` - String identifier
- `fullName` - User's full name
- `emoji` - An emoji representing the user

Sample users:
- John Doe 😀
- Jane Smith 🚀
- Robert Johnson 🎸

## Getting Started

### Prerequisites

- .NET 9.0 SDK or newer
- Visual Studio Code (recommended) with C# Dev Kit extension
- GitHub Copilot (for practice exercises)

### Installation

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd net-users-demo
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

### Running the Application

#### Using the solution file:
```bash
dotnet run --project net-users-api
```

#### Or navigate to the project directory:
```bash
cd net-users-api
dotnet run
```

The application will start on `http://localhost:8080`

### Testing the API

#### View HTML table:
```bash
open http://localhost:8080
```

#### Get all users (JSON):
```bash
curl http://localhost:8080/api/v1/users
```

#### Get a specific user:
```bash
curl http://localhost:8080/api/v1/users/1
```

#### Create a new user:
```bash
curl -X POST http://localhost:8080/api/v1/users \
  -H "Content-Type: application/json" \
  -d '{"id":"4", "fullName":"Alice Cooper", "emoji":"🎭"}'
```

#### Update a user:
```bash
curl -X PUT http://localhost:8080/api/v1/users/1 \
  -H "Content-Type: application/json" \
  -d '{"id":"1", "fullName":"John Smith", "emoji":"😎"}'
```

#### Delete a user (not yet implemented):
```bash
curl -X DELETE http://localhost:8080/api/v1/users/1
```

## Practice Exercises

This project includes two practice instruction files:

1. **Copilot_Practice_Instructions.md** - Learn to use GitHub Copilot's various features:
   - Inline code completion
   - Ask/Edit mode
   - Copilot CLI
   - Agent mode

2. **Copilot_TDD_Practice_Instructions.md** - Practice Test-Driven Development:
   - Using Testing custom chat mode
   - Using TDD custom chat mode
   - Red-Green-Refactor cycle
   - Test coverage analysis

## Comparison with Go Demo

This .NET implementation mirrors the [frye/go-users-demo](https://github.com/frye/go-users-demo) project:

| Feature | Go Demo | .NET Demo |
|---------|---------|-----------|
| Framework | Gin | ASP.NET Core |
| Language | Go | C# |
| View Engine | Go templates | Razor |
| Port | 8080 | 8080 |
| Data Storage | In-memory slice | In-memory List |
| DeleteUser | TODO | TODO |
| Sample Users | 3 users | 3 users (same) |

## Development

### Building the project:
```bash
dotnet build
```

### Running tests (after generating them):
```bash
dotnet test
```

### Cleaning build artifacts:
```bash
dotnet clean
```

## License

This project is for educational purposes.

## Contributing

This is a demonstration project for learning GitHub Copilot and TDD practices. Feel free to use it as a template for your own learning.
