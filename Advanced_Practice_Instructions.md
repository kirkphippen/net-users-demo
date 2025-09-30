# Advanced Practice Instructions for GitHub Copilot

This guide provides advanced exercises for practicing GitHub Copilot features with the net-users-demo API project. These tasks build upon the foundational exercises and introduce real-world scenarios involving CRUD operations, testing, security, observability, and architecture patterns.

## Table of Contents

- [Getting Started](#getting-started)
- [Module 1: Core CRUD & API Design Enhancements](#module-1-core-crud--api-design-enhancements)
- [Module 2: Validation & Error Handling](#module-2-validation--error-handling)
- [Module 3: Testing Expansion](#module-3-testing-expansion)
- [Interlude: Introduction to GitHub Spec-Kit](#interlude-introduction-to-github-spec-kit)
- [Module 4: Persistence & Data Layer Improvements](#module-4-persistence--data-layer-improvements)
- [Module 5: Configuration & Environment Management](#module-5-configuration--environment-management)
- [Module 6: Security & Authentication](#module-6-security--authentication)
- [Module 7: Observability & Operations](#module-7-observability--operations)
- [Module 8: Documentation & Developer Experience](#module-8-documentation--developer-experience)
- [Module 9: Tooling & Automation](#module-9-tooling--automation)
- [Module 10: Architecture & Scalability](#module-10-architecture--scalability)
- [Capstone Projects](#capstone-projects)

---

## Getting Started

Before beginning these exercises:

1. **Complete the basic practice instructions** in `Copilot_Practice_Instructions.md`
2. **Familiarize yourself with TDD** using `Copilot_TDD_Practice_Instructions.md`
3. **Ensure your development environment is ready**: .NET 9 SDK installed, project builds successfully
4. **Review the project structure** in `.github/copilot-instructions.md`

### How to Use This Guide

Each exercise includes:
- **Goal**: What you'll accomplish
- **Sample Prompt**: What to ask Copilot
- **Acceptance Criteria**: How to verify success
- **Stretch Goals**: Optional advanced variations

**Pro Tip**: Try solving exercises with different Copilot features:
- 💬 **Copilot Chat**: For planning and guidance
- ⚡ **Inline Suggestions**: For quick code completion
- 🔧 **Copilot Edits**: For multi-file refactoring
- 📝 **Test Generation**: For creating test cases

---

## Module 1: Core CRUD & API Design Enhancements

### Exercise 1.1: Implement Delete User Endpoint

**Goal**: Complete the missing DELETE endpoint (marked as TODO in the project)

**Sample Prompt**:
```
Create a DELETE endpoint at /api/v1/users/{id} in UsersController. 
Return 204 No Content on success, 404 Not Found if the user doesn't exist. 
Follow the existing pattern in the controller. Add logging for the operation.
```

**Acceptance Criteria**:
- [ ] Endpoint responds to DELETE requests at `/api/v1/users/{id}`
- [ ] Returns 204 status code when user is successfully deleted
- [ ] Returns 404 status code when user ID doesn't exist
- [ ] Logs the deletion operation with appropriate level
- [ ] Includes XML documentation comments

**Stretch Goals**:
- Implement soft delete with `DeletedAt` timestamp property
- Add ability to restore soft-deleted users
- Create unit tests for the delete operation

**Sample Prompt for Stretch**:
```
Refactor the delete endpoint to implement soft delete pattern. 
Add a DeletedAt nullable DateTime property to UserProfile. 
When deleting, set the timestamp instead of removing from the list. 
Update GetUsers to filter out soft-deleted users by default.
Add a query parameter includeDeleted=true to show all users.
```

---

### Exercise 1.2: Implement Partial Update (PATCH)

**Goal**: Add a PATCH endpoint for partial user updates

**Sample Prompt**:
```
Add a PATCH endpoint at /api/v1/users/{id} in UsersController. 
Accept a JSON object with optional fields: FullName and Emoji (but not Id). 
Only update fields that are provided in the request. 
Return 200 with updated user, 404 if not found, 400 for invalid data.
Use Microsoft.AspNetCore.JsonPatch if needed.
```

**Acceptance Criteria**:
- [ ] Endpoint responds to PATCH requests at `/api/v1/users/{id}`
- [ ] Updates only the fields provided in the request body
- [ ] Prevents modification of the `Id` field
- [ ] Returns appropriate status codes (200, 404, 400)
- [ ] Validates input data

**Stretch Goals**:
- Implement JSON Merge Patch (RFC 7396) vs JSON Patch (RFC 6902)
- Add optimistic concurrency control with ETag headers
- Support for undoing specific field updates

---

### Exercise 1.3: Add Pagination and Filtering

**Goal**: Enhance GET /users endpoint with pagination and filtering capabilities

**Sample Prompt**:
```
Enhance the GetUsers endpoint to support pagination and filtering.
Add query parameters: page (default 1), pageSize (default 10, max 100), 
sortBy (id/fullName/emoji), sortOrder (asc/desc).
Return results with pagination metadata: totalCount, currentPage, pageSize, totalPages.
Add a filter parameter: searchTerm to search in FullName.
```

**Acceptance Criteria**:
- [ ] Supports `page` and `pageSize` query parameters
- [ ] Supports `sortBy` and `sortOrder` parameters
- [ ] Returns pagination metadata in response headers or wrapped response
- [ ] Implements search filtering on FullName
- [ ] Validates page size limits (max 100)

**Stretch Goals**:
- Implement cursor-based pagination for better performance
- Add multiple filter parameters (emoji, id prefix)
- Support for complex filter expressions
- Add ETag support for caching
- Return `Link` headers for next/prev pages (RFC 5988)

**Sample Prompt for Cursor Pagination**:
```
Refactor pagination to use cursor-based approach instead of page numbers.
Add a cursor parameter (base64 encoded). Return a nextCursor in the response
that clients can use for the next page. Cursor should encode the last item's Id.
```

---

### Exercise 1.4: Retrieve User by Email

**Goal**: Add endpoint to find users by email address

**Sample Prompt**:
```
Add a GET endpoint at /api/v1/users/by-email to UsersController.
Accept an email query parameter and return the matching user.
Return 404 if no user found. This teaches lookup by unique field.
First, add an Email property to UserProfile model.
```

**Acceptance Criteria**:
- [ ] `Email` property added to `UserProfile` model
- [ ] Endpoint responds to GET at `/api/v1/users/by-email?email=...`
- [ ] Returns 200 with user object when found
- [ ] Returns 404 when email doesn't match any user
- [ ] Email is validated for proper format

**Stretch Goals**:
- Support partial/fuzzy email search
- Case-insensitive email matching
- Return multiple users if email domain matches
- Add email uniqueness validation on create/update

---

### Exercise 1.5: Bulk Create Users

**Goal**: Add endpoint to create multiple users in one request

**Sample Prompt**:
```
Create a POST endpoint at /api/v1/users/bulk in UsersController.
Accept an array of UserProfile objects. Validate each one.
If all are valid, create them all and return 201 with the created users.
If any validation fails, return 400 with detailed error information.
Log how many users were created in the bulk operation.
```

**Acceptance Criteria**:
- [ ] Endpoint accepts array of user objects
- [ ] All users created successfully or none (atomic operation)
- [ ] Returns 201 with array of created users
- [ ] Returns 400 with per-item validation errors
- [ ] Validates uniqueness of IDs across the batch

**Stretch Goals**:
- Support partial success mode with per-item status
- Add transaction simulation for rollback scenarios
- Implement batch size limits (e.g., max 100 per request)
- Add progress reporting for large batches

---

## Module 2: Validation & Error Handling

### Exercise 2.1: Centralized Error Model

**Goal**: Create consistent error response structure across all endpoints

**Sample Prompt**:
```
Create a centralized error response model in Models folder called ApiError.
Include properties: ErrorCode (string), Message (string), Details (optional object),
Timestamp (DateTime), Path (request path).
Create an exception filter or middleware to catch exceptions and return 
consistent ApiError responses. Map common exceptions to appropriate status codes.
```

**Acceptance Criteria**:
- [ ] `ApiError` model created with required properties
- [ ] Global exception handler implemented
- [ ] All endpoints return consistent error format
- [ ] HTTP status codes properly mapped
- [ ] Include request path and timestamp in errors

**Stretch Goals**:
- Implement RFC 7807 Problem Details (application/problem+json)
- Add correlation IDs for error tracking
- Create custom exception classes for domain errors
- Add stack trace in development environment only
- Implement error code enumeration/catalog

**Sample Prompt for RFC 7807**:
```
Refactor the error handling to use RFC 7807 Problem Details format.
Use Microsoft.AspNetCore.Mvc.ProblemDetails as the base.
Include type (URI), title, status, detail, instance, and custom extension members.
Configure the API to return application/problem+json content type for errors.
```

---

### Exercise 2.2: Comprehensive Input Validation

**Goal**: Add robust validation layer for all input data

**Sample Prompt**:
```
Add comprehensive validation to UserProfile model and API endpoints.
Use data annotations: Required, StringLength, EmailAddress, RegularExpression.
Email must be valid format. FullName must be 2-100 characters.
Emoji must be a single emoji character. Id must follow specific format (alphanumeric, 1-50 chars).
Return 400 Bad Request with specific validation errors listing each field problem.
```

**Acceptance Criteria**:
- [ ] Data annotations added to `UserProfile` properties
- [ ] Model validation executed automatically on requests
- [ ] Returns 400 with field-specific validation errors
- [ ] Error messages are clear and actionable
- [ ] Custom validators implemented where needed

**Stretch Goals**:
- Implement FluentValidation library for complex rules
- Add conditional validation (e.g., field required if another is set)
- Internationalize validation error messages
- Create reusable validation attributes
- Add business rule validation (e.g., no duplicate names)

**Sample Prompt for FluentValidation**:
```
Install and configure FluentValidation for ASP.NET Core.
Create UserProfileValidator class with validation rules.
Email is required and must be valid. FullName required, 2-100 chars, no numbers.
Emoji must be 1-10 characters. Id required, alphanumeric only.
Register validator with DI and integrate with model binding validation.
```

---

### Exercise 2.3: Rate Limiting Implementation

**Goal**: Protect API from abuse with rate limiting

**Sample Prompt**:
```
Implement a simple rate limiting middleware for the API.
Limit requests to 100 per minute per IP address.
Use in-memory token bucket or sliding window algorithm.
Return 429 Too Many Requests when limit exceeded, include Retry-After header.
Add configuration for limits in appsettings.json.
Exclude /health endpoint from rate limiting.
```

**Acceptance Criteria**:
- [ ] Rate limiting middleware created
- [ ] Tracks requests by IP address
- [ ] Returns 429 status with Retry-After header
- [ ] Configuration stored in appsettings
- [ ] Health check endpoint excluded
- [ ] Thread-safe implementation

**Stretch Goals**:
- Implement distributed rate limiting with Redis
- Add different limits per endpoint
- API key-based rate limiting (higher limits for authenticated users)
- Rate limit by user ID instead of IP
- Implement leaky bucket algorithm
- Add rate limit status headers (X-RateLimit-Limit, X-RateLimit-Remaining)

---

## Module 3: Testing Expansion

### Exercise 3.1: Comprehensive Unit Test Suite

**Goal**: Achieve high unit test coverage for controllers and models

**Sample Prompt**:
```
Create an xUnit test project at solution level: net-users-api.Tests.
Add tests for UsersController covering all endpoints: GetUsers, GetUser, 
CreateUser, UpdateUser, DeleteUser. Test happy paths and error cases.
Mock the logger. Use the Arrange-Act-Assert pattern.
Test edge cases: null inputs, invalid IDs, duplicate entries, empty lists.
```

**Acceptance Criteria**:
- [ ] Test project created with xUnit
- [ ] Tests for all controller methods
- [ ] Both success and failure paths covered
- [ ] Edge cases tested
- [ ] Tests use proper naming convention
- [ ] All tests pass

**Stretch Goals**:
- Achieve >80% code coverage
- Use theory/inline data for parameterized tests
- Add performance/benchmark tests
- Test concurrent access scenarios
- Add mutation testing
- Use AutoFixture for test data generation

**Sample Prompt for Advanced Testing**:
```
Enhance the test suite with Theory tests using InlineData for multiple scenarios.
Add a test class for testing pagination: various page sizes, boundary conditions,
invalid page numbers. Use AutoFixture to generate test data.
Add a benchmark test using BenchmarkDotNet to measure GetUsers performance.
```

---

### Exercise 3.2: Integration Testing

**Goal**: Create integration tests that exercise the full API stack

**Sample Prompt**:
```
Create integration tests using WebApplicationFactory in the test project.
Test the full HTTP request/response cycle for all endpoints.
Use a test fixture to setup and teardown the test server.
Test content negotiation, status codes, response headers, and JSON serialization.
Create a test data seeder for consistent test state.
```

**Acceptance Criteria**:
- [ ] Integration test class created
- [ ] Uses `WebApplicationFactory<Program>`
- [ ] Tests actual HTTP requests/responses
- [ ] Tests don't interfere with each other (isolated)
- [ ] Validates response status codes and content
- [ ] All integration tests pass

**Stretch Goals**:
- Use TestContainers for database integration
- Test with different content types (JSON, XML)
- Add tests for middleware pipeline
- Test authentication/authorization flows
- Add load testing scenarios
- Test graceful degradation

---

### Exercise 3.3: Property-Based Testing

**Goal**: Use property-based testing to find edge cases

**Sample Prompt**:
```
Add FsCheck or FsCheck.Xunit to the test project for property-based testing.
Create property tests for UserProfile validation: any valid user should
successfully serialize/deserialize, Id generation should always produce unique values,
email validation should accept all valid RFC formats.
Test that pagination always returns correct counts and never loses/duplicates items.
```

**Acceptance Criteria**:
- [ ] FsCheck or similar library added
- [ ] At least 3 property-based tests created
- [ ] Tests find edge cases not covered by example-based tests
- [ ] Properties are clearly defined and documented
- [ ] Tests run with sufficient iterations

**Stretch Goals**:
- Add shrinking to find minimal failing cases
- Test invariants that should always hold
- Use custom generators for domain objects
- Integrate fuzzing for input validation
- Create property tests for API contract compliance

---

## Interlude: Introduction to GitHub Spec-Kit

Before diving into more complex architectural patterns, let's explore **GitHub Spec-Kit** - a powerful tool for spec-driven development that can help you plan and implement features systematically with AI assistance.

### What is Spec-Kit?

**Spec-Kit** is a spec-driven development toolkit that helps you build high-quality software faster by focusing on **what** you want to build (specifications) before diving into **how** to build it (implementation). It provides structured workflows with slash commands that guide you through:

1. **Specification** - Define what you're building and why
2. **Planning** - Choose your tech stack and architecture
3. **Task Breakdown** - Generate actionable, dependency-ordered tasks
4. **Implementation** - Execute the plan systematically

### Why Use Spec-Kit?

- 📋 **Structured Approach**: Follow a proven workflow from idea to implementation
- 🤖 **AI-Powered**: Works seamlessly with GitHub Copilot, Claude, and other AI agents
- 📝 **Documentation-First**: Generate comprehensive specs, plans, and task lists
- 🔄 **Iterative Refinement**: Refine specifications before writing code
- ✅ **Test-Driven**: Built-in support for TDD workflows

### Getting Started with Spec-Kit

#### Installing uvx (Required)

Spec-Kit uses **uvx** (part of the uv package manager) to run without installation. Here's how to install it on common platforms:

**macOS/Linux**:
```bash
# Install uv (includes uvx)
curl -LsSf https://astral.sh/uv/install.sh | sh
```

**Windows (PowerShell)**:
```powershell
# Install uv (includes uvx)
powershell -c "irm https://astral.sh/uv/install.ps1 | iex"
```

**Alternative - Using pip**:
```bash
pip install uv
```

**Verify installation**:
```bash
uvx --version
```

For more installation options, see the [uv documentation](https://docs.astral.sh/uv/getting-started/installation/).

> **Can't install uvx?** See [Appendix: Spec-Kit Without uvx](#appendix-spec-kit-without-uvx) for alternative installation methods.

#### Installation in This Repository

To initialize Spec-Kit in your cloned net-users-demo repository:

```bash
# Navigate to your project root
cd /path/to/net-users-demo

# Initialize Spec-Kit in the current directory
uvx --from git+https://github.com/github/spec-kit.git specify init --here --ai copilot --force
```

**What these flags mean**:
- `--here`: Initialize in the current directory (instead of creating a new folder)
- `--ai copilot`: Configure for GitHub Copilot
- `--force`: Allow initialization in a non-empty directory

> **Note**: For other initialization options (new projects, different AI agents, persistent installation), see the [Spec-Kit Installation Guide](https://github.com/github/spec-kit/blob/main/docs/installation.md).

#### Prerequisites

Before running Spec-Kit, ensure you have:
- **Python 3.11+** installed
- **uv** package manager: `pip install uv` or follow [uv installation docs](https://docs.astral.sh/uv/)
- **Git** installed
- **GitHub Copilot** (or another supported AI agent)

#### Verification

After initialization, you should see:
- `.specify/` directory with templates and scripts
- New slash commands available: `/specify`, `/plan`, `/tasks`, `/implement`
- Scripts in both `.sh` (Bash) and `.ps1` (PowerShell) formats

To verify everything is set up correctly:

```bash
# Check that all required tools are installed
uvx --from git+https://github.com/github/spec-kit.git specify check
```

### Core Spec-Kit Workflow

#### 1. Establish Project Principles with `/constitution`

Before creating features, establish your project's governing principles:

```text
/constitution Create principles focused on C# best practices, RESTful API design, 
comprehensive testing, clean architecture, and maintainability. Include guidance 
for ASP.NET Core development and API documentation standards.
```

This creates `.specify/memory/constitution.md` with your project's foundational guidelines.

#### 2. Create Specification with `/specify`

Define **what** you want to build without worrying about the tech details:

```text
/specify Add user authentication to the API. Users should be able to register 
with email and password, log in to receive a JWT token, and use that token to 
access protected endpoints. Include password reset functionality and email verification.
```

This creates a detailed specification document in `specs/[feature-number]/spec.md`.

#### 3. Generate Technical Plan with `/plan`

Now specify **how** to build it - your tech stack and architecture:

```text
/plan Use ASP.NET Core Identity for user management with Entity Framework Core 
and PostgreSQL. Implement JWT bearer authentication with refresh tokens. 
Use FluentValidation for input validation. Add email service abstraction 
with SendGrid implementation. Follow the existing repository pattern.
```

This generates:
- `plan.md` - Overall implementation plan
- `data-model.md` - Entity definitions and relationships
- `contracts/` - API specifications (OpenAPI schemas)
- `research.md` - Technical decisions and library choices
- `quickstart.md` - Test scenarios

#### 4. Break Down into Tasks with `/tasks`

Generate an actionable, dependency-ordered task list:

```text
/tasks
```

This creates `tasks.md` with:
- Sequential tasks with clear dependencies
- Parallel execution opportunities marked
- Test-first approach for each task
- Acceptance criteria per task

#### 5. Execute Implementation with `/implement`

Execute the plan systematically:

```text
/implement
```

The AI agent will:
- Execute tasks in dependency order
- Follow TDD principles
- Run tests after each implementation
- Handle errors and provide progress updates

### Using Spec-Kit with Existing Features

You can use Spec-Kit to plan additions to the existing net-users-demo API:

**Sample Prompt for Spec-Kit Planning**:
```text
/specify Add pagination, filtering, and sorting capabilities to the existing 
GET /users endpoint. Support query parameters for page number, page size, 
search term filtering on FullName, and sorting by any field. Include 
pagination metadata in the response.
```

Then follow with:
```text
/plan Use LINQ for query building, add PaginationHelper class for 
consistent pagination logic across endpoints, return pagination metadata 
in custom headers (X-Total-Count, X-Page-Number, X-Page-Size). 
Add comprehensive validation for pagination parameters.
```

### Spec-Kit Resources

- 📘 **GitHub Repository**: [github.com/github/spec-kit](https://github.com/github/spec-kit)
- 📖 **Full Documentation**: [Spec-Driven Development Guide](https://github.com/github/spec-kit/blob/main/spec-driven.md)
- 🎥 **Video Overview**: [Watch on YouTube](https://www.youtube.com/watch?v=a9eR1xsfvHg)
- 💬 **Support**: [Open an Issue](https://github.com/github/spec-kit/issues/new)

### Exercise: Initialize Spec-Kit (Optional)

**Goal**: Set up Spec-Kit in your net-users-demo repository

**Steps**:
1. Ensure prerequisites are installed (Python 3.11+, uv, Git)
2. Navigate to your project root
3. Run: `uvx --from git+https://github.com/github/spec-kit.git specify init --here --ai copilot --force`
4. Verify: Check for `.specify/` directory and slash commands
5. Create constitution: `/constitution` with .NET/C# best practices

**Acceptance Criteria**:
- [ ] `.specify/` directory created with templates and scripts
- [ ] Slash commands available in GitHub Copilot Chat
- [ ] `constitution.md` created with project principles
- [ ] Scripts are executable (`.sh` files on Unix-like systems)

**Benefits**:
Once set up, you can use Spec-Kit slash commands for all subsequent exercises in this guide, providing a more structured and systematic approach to feature development.

---

## Module 4: Persistence & Data Layer Improvements

**Note**: The exercises in this module can be approached using traditional prompting or by using Spec-Kit slash commands for a more structured workflow.

### Exercise 4.1: Repository Pattern Implementation

**Goal**: Abstract data access behind repository interfaces

#### Approach 1: Traditional Prompting

**Sample Prompt**:
```
Implement the Repository pattern to abstract data access.
Create IUserRepository interface with methods: GetAll, GetById, Add, Update, Delete.
Create InMemoryUserRepository class that implements the interface using the existing static list.
Update UsersController to use IUserRepository through dependency injection.
Register the repository in Program.cs service collection.
```

#### Approach 2: Using Spec-Kit (Recommended)

**Step 1 - Specify**:
```text
/specify Refactor the user data access layer to use the Repository pattern.
Create an IUserRepository interface that abstracts all user data operations 
(GetAll, GetById, Add, Update, Delete). Implement an in-memory version that 
wraps the existing static list. Update the UsersController to depend on the 
interface rather than accessing the list directly. This will make it easier 
to swap data storage implementations in the future.
```

**Step 2 - Plan**:
```text
/plan Create IUserRepository interface in a new Repositories folder. Add async 
methods for all CRUD operations. Implement InMemoryUserRepository with 
thread-safe operations. Update UsersController constructor to inject 
IUserRepository. Register repository as singleton in Program.cs. 
Ensure all existing tests still pass. Follow SOLID principles, 
particularly dependency inversion.
```

**Step 3 - Generate Tasks & Implement**:
```text
/tasks
/implement
```

**Acceptance Criteria**:
- [ ] `IUserRepository` interface created
- [ ] `InMemoryUserRepository` implementation created
- [ ] Controller refactored to use repository
- [ ] DI configured in Program.cs
- [ ] All existing functionality works unchanged
- [ ] Repository methods are async

**Stretch Goals**:
- Create additional implementations (SQL, file-based)
- Add specification pattern for queries
- Implement Unit of Work pattern
- Add generic repository base class
- Create repository tests with mock implementation
- Add repository caching layer

**Stretch with Spec-Kit**:
```text
/specify Extend the repository pattern to support multiple storage backends.
Add a SqliteUserRepository implementation using Entity Framework Core. 
Allow configuration-based selection between InMemory and Sqlite storage. 
The application should be able to switch between storage types by changing 
appsettings.json without code modifications.

/plan Use Entity Framework Core with SQLite provider. Create UserDbContext 
with UserProfile entity. Add factory pattern (IUserRepositoryFactory) to 
create repositories based on configuration. Add StorageType enum and 
configuration section in appsettings.json. Include database migrations 
for SQLite. Add connection string configuration. Ensure proper disposal 
of database contexts.
```

---

### Exercise 4.2: Database Migrations

**Goal**: Implement proper database schema versioning

#### Approach 1: Traditional Prompting

**Sample Prompt**:
```
Add Entity Framework Core migrations to the project.
Install Microsoft.EntityFrameworkCore.Design and Tools packages.
Create initial migration for UserProfile entity with SQLite provider.
Add migration for adding Email property. Add migration for soft delete support.
Create a database initialization/seeding strategy for development data.
Document the migration workflow in README.
```

#### Approach 2: Using Spec-Kit

**Spec-Kit Workflow**:
```text
/specify Add database migration support to the project using Entity Framework Core.
Implement a proper migration strategy for schema versioning. Create an initial 
migration for the UserProfile entity. Add subsequent migrations for schema evolution 
(Email property, soft delete support). Include database seeding for development data. 
Document the migration workflow for other developers.

/plan Use Entity Framework Core with SQLite provider. Install required packages: 
Microsoft.EntityFrameworkCore.Design and Microsoft.EntityFrameworkCore.Tools. 
Create UserDbContext with UserProfile entity. Generate initial migration using 
dotnet ef migrations add. Create HasData configuration for seed data. 
Add migration documentation to README with commands for add, update, remove. 
Include appsettings configuration for connection strings.

/tasks
/implement
```

**Acceptance Criteria**:
- [ ] EF Core packages installed
- [ ] DbContext created with UserProfile DbSet
- [ ] Initial migration created and applied
- [ ] Additional migrations for schema changes
- [ ] Seed data configured
- [ ] Migration commands documented

**Stretch Goals**:
- Add automatic migration on startup (dev only)
- Implement migration rollback testing
- Add migration history tracking
- Create data migration scripts
- Add idempotent migration support
- Implement blue-green deployment migrations

---

### Exercise 4.3: Optimistic Concurrency Control

**Goal**: Prevent concurrent update conflicts

#### Approach 1: Traditional Prompting

**Sample Prompt**:
```
Add optimistic concurrency control to prevent lost updates.
Add a Version or RowVersion property to UserProfile (timestamp or int).
Increment version on each update. Accept If-Match header with version in PUT requests.
Return 409 Conflict if version doesn't match (concurrent update detected).
Include current version in ETag response header for GET requests.
```

#### Approach 2: Using Spec-Kit

**Spec-Kit Workflow**:
```text
/specify Implement optimistic concurrency control to prevent lost updates in 
concurrent scenarios. Add versioning to UserProfile entities. When a user is 
updated, verify the version hasn't changed since it was read. If concurrent 
modification is detected, return an appropriate error. Use HTTP ETags to 
communicate version information to clients following REST best practices.

/plan Add RowVersion property to UserProfile (byte array with [Timestamp] attribute). 
Configure EF Core concurrency token. Update PUT endpoint to accept If-Match header 
with ETag. Compare ETag with current RowVersion. Return 409 Conflict with helpful 
message if mismatch. Add ETag to response headers for GET requests. 
Handle DbUpdateConcurrencyException. Add middleware to automatically set ETag headers. 
Create integration tests for concurrent update scenarios.

/tasks
/implement
```

**Acceptance Criteria**:
- [ ] Version property added to model
- [ ] Version checked before updates
- [ ] Returns 409 on version mismatch
- [ ] ETag headers implemented
- [ ] Version included in responses
- [ ] Tests verify conflict detection

**Stretch Goals**:
- Implement automatic retry with exponential backoff
- Add conflict resolution strategies
- Support weak vs strong ETags
- Implement version history tracking
- Add precondition checks (If-Unmodified-Since)

---

### 💡 Spec-Kit for Remaining Modules

**You've now learned both traditional prompting and Spec-Kit workflows!**

For the remaining modules (5-10), you can apply either approach. We recommend using Spec-Kit's structured workflow (`/specify` → `/plan` → `/tasks` → `/implement`) for:

- **Complex multi-component features** (e.g., authentication systems, observability stacks)
- **Features requiring detailed planning** (e.g., multi-environment configs, CI/CD pipelines)
- **When you want comprehensive documentation** (specs, plans, tasks all generated automatically)

**Pro Tip**: You can mix approaches! Use Spec-Kit for initial planning and architecture, then use direct prompts for quick tactical changes.

---

## Module 5: Configuration & Environment Management

### Exercise 5.1: Structured Configuration Management

**Goal**: Implement robust configuration system

**Sample Prompt**:
```
Create a strongly-typed configuration system for the application.
Create AppSettings class with nested sections: Database, Logging, RateLimit, Security.
Bind configuration from appsettings.json, environment variables, and command line.
Add configuration validation on startup (fail fast if invalid).
Support configuration hot reload for specific settings.
Use IOptions pattern for injecting configuration.
```

**Acceptance Criteria**:
- [ ] Configuration classes created with properties
- [ ] Configuration bound from multiple sources
- [ ] Validation implemented with data annotations
- [ ] Configuration injected via IOptions
- [ ] Startup validation fails fast
- [ ] Configuration precedence documented

**Stretch Goals**:
- Implement feature flags configuration
- Add Azure Key Vault configuration provider
- Create configuration change listeners
- Add configuration encryption for secrets
- Implement configuration versioning
- Add configuration documentation generator

---

### Exercise 5.2: Secrets Management

**Goal**: Implement secure handling of sensitive configuration

**Sample Prompt**:
```
Implement proper secrets management for the application.
Use .NET User Secrets for local development. Add .env file support.
Create a secrets template file (.env.example) with all required secrets.
Add secret validation on startup. Document secret rotation procedures.
Show how to use Azure Key Vault or AWS Secrets Manager in production.
Ensure secrets never appear in logs or error messages.
```

**Acceptance Criteria**:
- [ ] User Secrets configured for development
- [ ] .env support added (if needed)
- [ ] .env.example template created
- [ ] Secrets validation implemented
- [ ] Production secrets documentation added
- [ ] Secret scrubbing in logs

**Stretch Goals**:
- Implement automatic secret rotation
- Add secrets health checks
- Create secrets audit logging
- Implement secret versioning
- Add emergency secret revocation
- Create secrets access control

---

### Exercise 5.3: Multi-Environment Configuration

**Goal**: Support different configurations per environment

**Sample Prompt**:
```
Create distinct configuration profiles for Development, Staging, and Production.
Add appsettings.Staging.json and enhance appsettings.Production.json.
Configure different settings: log levels, connection strings, CORS policies,
rate limits, feature flags. Add environment indicator to API responses (header).
Document environment-specific setup requirements.
```

**Acceptance Criteria**:
- [ ] Environment-specific appsettings files created
- [ ] Configuration varies appropriately per environment
- [ ] Environment detection working correctly
- [ ] API indicates current environment
- [ ] Environment setup documented
- [ ] Environment variables override files

**Stretch Goals**:
- Implement infrastructure-as-code for environments
- Add environment smoke tests
- Create environment promotion pipeline
- Implement blue-green deployment support
- Add canary deployment configuration
- Create environment comparison tools

---

## Module 6: Security & Authentication

### Exercise 6.1: API Key Authentication

**Goal**: Implement basic API key authentication

**Sample Prompt**:
```
Implement API key authentication for the users API.
Clients must provide X-API-Key header with valid key.
Store API keys in configuration (for now). Create an authentication middleware
that validates the key. Return 401 Unauthorized if missing/invalid.
Allow unauthenticated access to /health endpoint.
Add multiple API keys with different permissions (read-only vs full access).
```

**Acceptance Criteria**:
- [ ] Authentication middleware created
- [ ] X-API-Key header validated
- [ ] Returns 401 for invalid/missing keys
- [ ] Configuration stores valid keys
- [ ] Health endpoint remains public
- [ ] Tests verify authentication

**Stretch Goals**:
- Implement API key management endpoints
- Add key rotation mechanism
- Support key expiration
- Implement rate limits per API key
- Add API key usage analytics
- Create key hashing/storage security

---

### Exercise 6.2: JWT-Based Authentication

**Goal**: Implement modern token-based authentication

**Sample Prompt**:
```
Implement JWT authentication for the API.
Add a /api/v1/auth/login endpoint that accepts username/password
and returns a JWT token. Protect user endpoints with [Authorize] attribute.
Configure JWT bearer authentication in Program.cs with signing key from config.
Add token expiration (15 minutes). Include user ID and role in JWT claims.
Add /api/v1/auth/refresh endpoint for token renewal.
```

**Acceptance Criteria**:
- [ ] JWT authentication configured
- [ ] Login endpoint returns valid tokens
- [ ] Protected endpoints require valid tokens
- [ ] Token expiration works correctly
- [ ] Claims included in tokens
- [ ] Refresh token endpoint implemented

**Stretch Goals**:
- Implement refresh token rotation
- Add token revocation/blacklist
- Support multiple token audiences
- Implement sliding expiration
- Add OAuth2/OpenID Connect flows
- Create token introspection endpoint

**Sample Prompt for OAuth2**:
```
Enhance the authentication to support OAuth2 authorization code flow.
Install Microsoft.AspNetCore.Authentication.OAuth package.
Configure GitHub or Google as OAuth provider. Add callback endpoint
for OAuth redirect. Store provider tokens securely. Support multiple providers.
```

---

### Exercise 6.3: Input Security Hardening

**Goal**: Protect against common security vulnerabilities

**Sample Prompt**:
```
Implement comprehensive input security measures.
Add request size limits (max 1MB). Implement input sanitization for string fields.
Add SQL injection prevention (demonstrate with parameterized queries).
Implement XSS protection headers. Add CSRF protection for state-changing operations.
Create security middleware that adds security headers: X-Content-Type-Options,
X-Frame-Options, X-XSS-Protection, Strict-Transport-Security.
```

**Acceptance Criteria**:
- [ ] Request size limits enforced
- [ ] Input validation prevents injection attacks
- [ ] Security headers added to responses
- [ ] CSRF tokens implemented
- [ ] Content Security Policy configured
- [ ] Security tests pass

**Stretch Goals**:
- Implement Content Security Policy (CSP)
- Add CORS configuration with whitelist
- Implement request signing
- Add IP whitelist/blacklist
- Create security audit logging
- Implement honeypot fields

---

### Exercise 6.4: Audit Logging

**Goal**: Create comprehensive audit trail for sensitive operations

**Sample Prompt**:
```
Implement audit logging for all user creation, modification, and deletion.
Create AuditLog model with: Id, Timestamp, UserId, Action, EntityType, EntityId,
OldValue (JSON), NewValue (JSON), IpAddress, UserAgent.
Store audit logs separately from application logs.
Create a repository for audit logs. Add endpoint to query audit history.
Ensure audit logs are tamper-evident (add hash chain).
```

**Acceptance Criteria**:
- [ ] AuditLog model created
- [ ] Audit logging middleware/filter implemented
- [ ] All CUD operations logged
- [ ] Audit logs stored separately
- [ ] Query endpoint for audit history
- [ ] Includes contextual information (IP, user)

**Stretch Goals**:
- Implement audit log signing
- Add audit log encryption
- Create hash chain for tamper detection
- Implement audit log retention policies
- Add compliance reporting (GDPR, HIPAA)
- Create audit log export functionality

---

## Module 7: Observability & Operations

### Exercise 7.1: Structured Logging with Context

**Goal**: Implement comprehensive structured logging

**Sample Prompt**:
```
Enhance application logging to use structured logging throughout.
Add Serilog with JSON formatter. Include context in all logs: RequestId,
CorrelationId, UserId, Method, Path, StatusCode, Duration.
Create logging middleware that captures request/response details.
Log to both console and file (rolling file per day).
Use different log levels appropriately: Debug, Information, Warning, Error.
Add correlation ID propagation across service boundaries.
```

**Acceptance Criteria**:
- [ ] Serilog configured with JSON output
- [ ] Request/response logging middleware added
- [ ] Correlation IDs implemented
- [ ] Contextual properties included in logs
- [ ] Multiple sinks configured (console, file)
- [ ] Log levels used appropriately

**Stretch Goals**:
- Send logs to centralized system (Elasticsearch, Seq)
- Implement log sampling for high-volume endpoints
- Add sensitive data masking in logs
- Create log aggregation dashboards
- Implement distributed tracing correlation
- Add log-based alerting

---

### Exercise 7.2: Metrics and Monitoring

**Goal**: Expose application metrics for monitoring

**Sample Prompt**:
```
Implement application metrics using App.Metrics or Prometheus.NET.
Expose /metrics endpoint in Prometheus format. Track metrics:
- Request count (by endpoint, status code)
- Request duration histogram
- Active requests gauge
- User creation rate
- Error rate by type
Add custom business metrics: total users, users created today.
Create a metrics middleware to automatically track HTTP metrics.
```

**Acceptance Criteria**:
- [ ] Metrics library installed and configured
- [ ] /metrics endpoint exposes Prometheus format
- [ ] HTTP request metrics tracked automatically
- [ ] Custom business metrics implemented
- [ ] Metrics middleware created
- [ ] Documentation for available metrics

**Stretch Goals**:
- Create Grafana dashboards for metrics
- Implement metric alerting rules
- Add percentile tracking (p50, p95, p99)
- Track database query metrics
- Implement custom metric labels
- Add metric cardinality limits
- Create SLO/SLI definitions

**Sample Prompt for Grafana**:
```
Create Grafana dashboard JSON definition for the API metrics.
Include panels for: request rate, error rate, response time percentiles,
active users count, endpoint-specific latency. Add variables for time range
and endpoint filtering. Include alerting rules for error rate > 5%.
```

---

### Exercise 7.3: Distributed Tracing

**Goal**: Implement distributed tracing for request flows

**Sample Prompt**:
```
Add OpenTelemetry instrumentation to the application.
Install OpenTelemetry.Instrumentation.AspNetCore package.
Create spans for: HTTP requests, repository operations, validation.
Add custom attributes to spans: userId, endpoint, queryParams.
Configure export to Jaeger or Zipkin (optional, can be no-op exporter).
Propagate trace context in W3C Trace Context format.
```

**Acceptance Criteria**:
- [ ] OpenTelemetry configured
- [ ] Automatic instrumentation for ASP.NET Core
- [ ] Custom spans for repository operations
- [ ] Span attributes include relevant context
- [ ] Trace context propagation works
- [ ] Tracing can be enabled/disabled via config

**Stretch Goals**:
- Add baggage for cross-cutting concerns
- Implement trace sampling strategies
- Add trace-based error analysis
- Create service dependency maps
- Implement trace-driven testing
- Add custom span events
- Create trace retention policies

---

### Exercise 7.4: Health Checks

**Goal**: Implement comprehensive health monitoring

**Sample Prompt**:
```
Add health check endpoints to the application.
Create /health/live endpoint (liveness probe) - always returns 200 if app is running.
Create /health/ready endpoint (readiness probe) - checks dependencies are available.
Add health checks for: memory usage, database connectivity (when added).
Return 503 Service Unavailable if not ready. Include health check details
in response JSON. Add a degraded state for partial availability.
```

**Acceptance Criteria**:
- [ ] Health check middleware configured
- [ ] /health/live endpoint returns liveness status
- [ ] /health/ready endpoint checks dependencies
- [ ] Returns appropriate status codes
- [ ] Health check results include details
- [ ] Degraded state supported

**Stretch Goals**:
- Add custom health checks for external dependencies
- Implement health check caching
- Add health check history tracking
- Create health-based auto-scaling triggers
- Implement circuit breaker pattern
- Add health check publishing (to monitoring system)
- Create health check dashboards

---

## Module 8: Documentation & Developer Experience

### Exercise 8.1: OpenAPI/Swagger Enhancement

**Goal**: Create comprehensive API documentation

**Sample Prompt**:
```
Enhance the OpenAPI/Swagger documentation for the API.
Add XML documentation comments to all controllers and models.
Configure Swagger UI with API description, version, contact info, license.
Add example requests and responses to endpoints.
Document all possible status codes for each endpoint.
Add authentication/authorization documentation to Swagger.
Organize endpoints with tags. Add operation IDs for client generation.
```

**Acceptance Criteria**:
- [ ] XML documentation enabled in project
- [ ] All public APIs have XML comments
- [ ] Swagger UI shows comprehensive docs
- [ ] Example requests/responses included
- [ ] Status codes documented
- [ ] Authentication documented in Swagger

**Stretch Goals**:
- Generate client SDKs (TypeScript, C#, Python)
- Add request/response schemas
- Implement API versioning in OpenAPI
- Add OpenAPI validation in tests
- Create Postman collection from OpenAPI
- Add API changelog documentation
- Implement contract testing

**Sample Prompt for Client Generation**:
```
Use NSwag or Swashbuckle to generate a TypeScript client SDK from OpenAPI spec.
Configure client generation in the build process. Generate models and API client.
Add a sample TypeScript project that demonstrates using the generated client.
Include error handling, retry logic, and type safety in the client.
```

---

### Exercise 8.2: Comprehensive README

**Goal**: Create excellent developer documentation

**Sample Prompt**:
```
Create a comprehensive README.md for the project with the following sections:
- Project overview with badges (build status, coverage, version)
- Quick start guide (prerequisites, installation, running)
- Architecture overview with diagram
- API endpoints documentation
- Development setup instructions
- Testing strategy and how to run tests
- Deployment guide
- Contributing guidelines
- Troubleshooting section
- License and contact information
Use Markdown formatting effectively with code blocks, tables, and links.
```

**Acceptance Criteria**:
- [ ] README covers all essential sections
- [ ] Clear step-by-step quick start
- [ ] Architecture diagram included
- [ ] Code examples are accurate
- [ ] Links to additional resources
- [ ] Properly formatted Markdown

**Stretch Goals**:
- Create additional documentation files (ARCHITECTURE.md, CONTRIBUTING.md)
- Add interactive API documentation
- Create video walkthroughs
- Add documentation for common use cases
- Implement documentation versioning
- Add localized documentation
- Create developer onboarding checklist

---

### Exercise 8.3: Architecture Decision Records (ADRs)

**Goal**: Document important architectural decisions

**Sample Prompt**:
```
Create an ADR (Architecture Decision Record) system for the project.
Create docs/adr directory. Add template file: 0000-adr-template.md.
Create first ADR: "0001-use-in-memory-storage.md" documenting why
we use static list instead of database for this demo project.
Create second ADR: "0002-rest-api-design.md" documenting API design choices.
Follow the format: Title, Status, Context, Decision, Consequences.
Add index file listing all ADRs.
```

**Acceptance Criteria**:
- [ ] ADR directory structure created
- [ ] Template file created
- [ ] At least 2 ADRs written
- [ ] ADRs follow consistent format
- [ ] ADR index file maintained
- [ ] ADRs linked from main README

**Stretch Goals**:
- Add ADRs for: authentication choice, logging framework, test strategy
- Create ADR status workflow (proposed, accepted, deprecated, superseded)
- Link ADRs to related code sections
- Add decision review dates
- Create ADR visualization/timeline
- Implement ADR CLI tool

---

### Exercise 8.4: Contributing Guide

**Goal**: Make it easy for others to contribute

**Sample Prompt**:
```
Create a CONTRIBUTING.md file with guidelines for contributing to the project.
Include sections for: code of conduct, how to report bugs, how to request features,
development workflow (branch naming, commit messages, PR process),
code style guidelines, testing requirements, documentation requirements.
Add a pull request template. Add issue templates for bug reports and features.
Include a checklist for PR reviews.
```

**Acceptance Criteria**:
- [ ] CONTRIBUTING.md created
- [ ] Clear contribution workflow documented
- [ ] Code style guidelines specified
- [ ] PR template created in .github folder
- [ ] Issue templates created
- [ ] Review checklist included

**Stretch Goals**:
- Add automated PR checks (linting, tests)
- Create contributor recognition system
- Add first-timers-only issues
- Implement CLA (Contributor License Agreement)
- Add release notes guidelines
- Create mentorship program docs
- Add documentation contribution guide

---

## Module 9: Tooling & Automation

### Exercise 9.1: Build Automation with Make/Script

**Goal**: Simplify common development tasks

**Sample Prompt**:
```
Create a Makefile (or build.sh script for cross-platform) with targets for:
- build: Compile the solution
- test: Run all tests with coverage
- run: Start the application
- clean: Remove build artifacts
- restore: Restore NuGet packages
- lint: Run code analysis
- format: Format code
- docker-build: Build Docker image
Add help target that lists all available targets.
Make targets idempotent and composable.
```

**Acceptance Criteria**:
- [ ] Makefile or build script created
- [ ] All common tasks automated
- [ ] Help documentation included
- [ ] Works on all target platforms
- [ ] Properly handles errors
- [ ] Documents any prerequisites

**Stretch Goals**:
- Add watch mode for auto-rebuild
- Implement parallel execution
- Add pre-commit hooks
- Create release automation
- Add dependency update checks
- Implement task caching
- Create task dependency graph

---

### Exercise 9.2: CI/CD Pipeline

**Goal**: Automate build, test, and deployment

**Sample Prompt**:
```
Create a GitHub Actions workflow for CI/CD in .github/workflows/ci.yml.
Trigger on push to main and pull requests. Jobs:
1. Build - restore, build, run tests
2. Test - run unit and integration tests, publish coverage
3. Analyze - run code quality checks (SonarCloud optional)
4. Package - build Docker image
5. Deploy - deploy to staging (can be manual approval)
Use matrix strategy to test on multiple .NET versions.
Cache dependencies for faster builds. Add status badges to README.
```

**Acceptance Criteria**:
- [ ] GitHub Actions workflow created
- [ ] Build and test steps working
- [ ] Runs on PRs and main branch
- [ ] Test results published
- [ ] Build artifacts saved
- [ ] Status badges in README

**Stretch Goals**:
- Add deployment to multiple environments
- Implement semantic versioning
- Add release automation
- Create deployment rollback capability
- Add smoke tests after deployment
- Implement feature flag deployments
- Add security scanning (Dependabot, CodeQL)

**Sample Prompt for Advanced Pipeline**:
```
Enhance the CI/CD pipeline with advanced features:
- Add CodeQL security scanning
- Implement semantic release with conventional commits
- Add Docker image signing and scanning
- Create separate staging and production deployment jobs with approvals
- Add rollback job in case deployment fails smoke tests
- Publish NuGet package on release
- Send notifications to Slack on build status
```

---

### Exercise 9.3: Containerization

**Goal**: Package application as Docker container

**Sample Prompt**:
```
Create a production-ready Dockerfile for the application.
Use multi-stage build: restore, build, publish, runtime.
Use official .NET SDK and ASP.NET runtime images.
Optimize image size (use alpine or distroless base).
Set non-root user for security. Expose port 8080.
Add health check instruction. Include build args for version info.
Create docker-compose.yml for local development with dependencies.
Create .dockerignore file.
```

**Acceptance Criteria**:
- [ ] Dockerfile created with multi-stage build
- [ ] Image builds successfully
- [ ] Container runs application correctly
- [ ] Image size optimized
- [ ] Runs as non-root user
- [ ] docker-compose.yml for local dev
- [ ] .dockerignore configured

**Stretch Goals**:
- Create separate debug and release images
- Add Docker build caching optimization
- Implement image scanning for vulnerabilities
- Create Kubernetes deployment manifests
- Add SBOM generation
- Implement image signing
- Create Docker Compose for full stack

**Sample Prompt for Kubernetes**:
```
Create Kubernetes deployment manifests for the application.
Include: Deployment, Service (ClusterIP), Ingress, ConfigMap, Secret.
Configure resource limits and requests. Add liveness and readiness probes.
Use environment-specific overlays with Kustomize.
Add HorizontalPodAutoscaler for auto-scaling. Document deployment commands.
```

---

### Exercise 9.4: Code Quality Tools

**Goal**: Enforce code quality standards

**Sample Prompt**:
```
Set up comprehensive code quality tooling for the project.
Configure .editorconfig with C# coding conventions.
Add .NET analyzers (Microsoft.CodeAnalysis.NetAnalyzers).
Configure StyleCop or Roslynator for additional rules.
Set warnings as errors for critical issues. Add code coverage threshold (80%).
Create pre-commit Git hook that runs format and lint.
Add SonarCloud or similar for static analysis (optional).
Document how to run quality checks locally.
```

**Acceptance Criteria**:
- [ ] .editorconfig configured
- [ ] Analyzers installed and configured
- [ ] Linting rules enforced
- [ ] Format checking automated
- [ ] Coverage thresholds set
- [ ] Pre-commit hooks installed
- [ ] Quality checks in CI

**Stretch Goals**:
- Add custom analyzers for domain rules
- Implement code complexity metrics
- Add duplicate code detection
- Create custom code fix providers
- Implement architecture rules (ArchUnit)
- Add technical debt tracking
- Create code review checklists

---

## Module 10: Architecture & Scalability

### Exercise 10.1: Response Caching

**Goal**: Improve performance with caching

**Sample Prompt**:
```
Implement response caching for the GET users endpoints.
Add in-memory caching for GetUsers and GetUser(id) responses.
Set appropriate cache duration (5 minutes for GetUsers, 15 min for GetUser).
Add cache invalidation on Create, Update, Delete operations.
Add X-Cache header to indicate cache hit/miss. Support Cache-Control
request headers (no-cache, max-age). Add configuration for cache TTL.
Implement cache warming strategy for frequently accessed data.
```

**Acceptance Criteria**:
- [ ] Response caching middleware configured
- [ ] GET endpoints cached with appropriate TTL
- [ ] Cache invalidated on mutations
- [ ] Cache headers included in responses
- [ ] Cache-Control headers respected
- [ ] Configuration for cache settings

**Stretch Goals**:
- Implement distributed caching with Redis
- Add cache partitioning by user
- Implement cache compression
- Add cache statistics endpoint
- Create cache monitoring dashboard
- Implement cache warming on startup
- Add cache stampede prevention

**Sample Prompt for Redis**:
```
Replace in-memory caching with Redis distributed cache.
Install StackExchange.Redis package. Configure Redis connection.
Implement cache-aside pattern for user data. Add serialization/deserialization.
Handle Redis connection failures gracefully (fallback to source).
Add Redis health check. Support cache tagging for bulk invalidation.
```

---

### Exercise 10.2: Asynchronous Processing

**Goal**: Implement background job processing

**Sample Prompt**:
```
Add background job processing using Hangfire or similar.
When a user is created, enqueue a background job to send welcome email.
Create IEmailService interface with mock implementation that logs.
Configure Hangfire with in-memory storage (or SQL if available).
Add Hangfire dashboard at /hangfire (secured). Create recurring job
to cleanup old users (soft-deleted > 30 days). Add job retry policy.
Log job execution and failures.
```

**Acceptance Criteria**:
- [ ] Background job framework installed
- [ ] Jobs enqueued on user creation
- [ ] Job processing working correctly
- [ ] Dashboard accessible and secured
- [ ] Recurring jobs configured
- [ ] Job failures handled with retries

**Stretch Goals**:
- Implement job prioritization
- Add job scheduling (delayed execution)
- Create job status tracking
- Implement job cancellation
- Add batch job processing
- Create job monitoring alerts
- Implement dead letter queue

---

### Exercise 10.3: Graceful Shutdown

**Goal**: Handle application shutdown properly

**Sample Prompt**:
```
Implement graceful shutdown handling for the application.
Register IHostApplicationLifetime events. On shutdown signal (SIGTERM):
1. Stop accepting new requests
2. Wait for in-flight requests to complete (max 30 seconds)
3. Close database connections
4. Flush logs
5. Complete background jobs
Add /admin/shutdown endpoint for manual shutdown (authenticated).
Log shutdown events. Test with container stop command.
```

**Acceptance Criteria**:
- [ ] Shutdown events registered
- [ ] Stops accepting new requests
- [ ] Waits for in-flight requests
- [ ] Resources cleaned up properly
- [ ] Shutdown timeouts configured
- [ ] Shutdown logged appropriately

**Stretch Goals**:
- Implement pre-shutdown hooks
- Add shutdown coordination for replicas
- Create shutdown health check state
- Implement connection draining
- Add shutdown notifications
- Create graceful restart mechanism
- Test chaos engineering scenarios

---

### Exercise 10.4: Horizontal Scaling Considerations

**Goal**: Prepare application for multi-instance deployment

**Sample Prompt**:
```
Analyze and refactor the application for horizontal scaling.
Issues to address:
1. Static list storage - needs shared storage (database/Redis)
2. In-memory cache - needs distributed cache
3. Background jobs - need distributed job coordination
4. Session state - must be stateless or externalized
Create a document analyzing scaling bottlenecks and solutions.
Implement at least one fix (e.g., replace static list with repository pattern).
Add load balancer simulation with multiple instances.
```

**Acceptance Criteria**:
- [ ] Scaling analysis document created
- [ ] Shared state identified and documented
- [ ] At least one scaling issue fixed
- [ ] Application tested with multiple instances
- [ ] Session management addressed
- [ ] Recommendations documented

**Stretch Goals**:
- Implement distributed locking
- Add sticky session alternatives
- Create instance coordination mechanism
- Implement rate limiting across instances
- Add distributed tracing correlation
- Create capacity planning guide
- Implement auto-scaling triggers

---

## Capstone Projects

### Capstone 1: Complete Modernization

**Goal**: Apply all learned concepts to fully modernize the API

**Sample Prompt**:
```
Perform a complete modernization of the net-users-demo API incorporating:
1. Replace in-memory storage with Entity Framework Core + PostgreSQL
2. Add JWT authentication with refresh tokens
3. Implement comprehensive validation with FluentValidation
4. Add distributed caching with Redis
5. Implement OpenTelemetry tracing and Prometheus metrics
6. Add background jobs with Hangfire
7. Create full test suite (unit, integration, load tests)
8. Add CI/CD pipeline with Docker and Kubernetes deployment
9. Create comprehensive documentation (OpenAPI, ADRs, README)
10. Implement all security best practices
Document architecture decisions and provide migration guide.
```

**Deliverables**:
- [ ] Fully modernized codebase
- [ ] Database with migrations
- [ ] Complete test coverage (>80%)
- [ ] CI/CD pipeline functional
- [ ] Docker and K8s manifests
- [ ] Comprehensive documentation
- [ ] Architecture decision records
- [ ] Performance benchmarks

---

### Capstone 2: Multi-Tenant SaaS Platform

**Goal**: Convert the API into a multi-tenant SaaS platform

**Sample Prompt**:
```
Transform the API into a multi-tenant SaaS platform:
1. Add Tenant model and tenant_id to all entities
2. Implement tenant isolation at data layer
3. Add tenant registration and management API
4. Implement per-tenant rate limiting
5. Add tenant-specific configuration
6. Create tenant admin dashboard
7. Implement usage tracking and billing integration
8. Add tenant-level analytics
9. Create tenant onboarding workflow
10. Implement tenant data export/deletion (GDPR compliance)
Support both shared-database and database-per-tenant strategies.
```

**Deliverables**:
- [ ] Tenant management system
- [ ] Data isolation working correctly
- [ ] Tenant registration flow
- [ ] Admin dashboard
- [ ] Usage tracking system
- [ ] GDPR compliance features
- [ ] Documentation for tenant architecture
- [ ] Migration path from single-tenant

---

### Capstone 3: Event-Driven Microservices

**Goal**: Decompose into event-driven microservices

**Sample Prompt**:
```
Refactor the monolithic API into event-driven microservices:
1. Identify bounded contexts: User Service, Notification Service, Analytics Service
2. Implement event bus (RabbitMQ or Azure Service Bus)
3. Define domain events: UserCreated, UserUpdated, UserDeleted
4. Create service-to-service communication patterns
5. Implement saga pattern for distributed transactions
6. Add API gateway (Ocelot or YARP)
7. Implement service discovery
8. Add distributed tracing across services
9. Create resilience patterns (circuit breaker, retry, timeout)
10. Implement event sourcing for audit trail
Document microservices architecture and deployment topology.
```

**Deliverables**:
- [ ] Multiple microservices deployed
- [ ] Event bus configured
- [ ] Domain events published/consumed
- [ ] API gateway functional
- [ ] Service resilience implemented
- [ ] Distributed tracing working
- [ ] Architecture documentation
- [ ] Deployment diagrams

---

## Tips for Success

### Using GitHub Copilot Effectively

1. **Be Specific**: Provide detailed context in your prompts
2. **Iterate**: Start with basic implementation, then enhance
3. **Review**: Always review generated code for quality and security
4. **Test**: Use Copilot to generate tests alongside implementation
5. **Learn**: Understand the code generated, don't just copy-paste

### Working Through Exercises

1. **Plan First**: Understand requirements before coding
2. **Test-Driven**: Write tests before or alongside implementation
3. **Small Steps**: Break large exercises into smaller tasks
4. **Commit Often**: Make small, focused commits
5. **Document**: Add comments and documentation as you go

### Getting Unstuck

1. **Ask Copilot for Help**: "Explain how to...", "What's wrong with..."
2. **Review Examples**: Look at existing code in the project
3. **Check Documentation**: Refer to .NET and library docs
4. **Simplify**: Break the problem into smaller pieces
5. **Experiment**: Try different approaches

### Best Practices

- ✅ Follow the project's coding conventions
- ✅ Write tests for all new functionality
- ✅ Add XML documentation comments
- ✅ Use proper error handling
- ✅ Log important operations
- ✅ Update README with new features
- ✅ Create ADRs for architectural decisions

---

## Additional Resources

### .NET Documentation
- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [xUnit Testing](https://xunit.net/)

### GitHub Copilot Resources
- [GitHub Copilot Documentation](https://docs.github.com/en/copilot)
- [Copilot Chat Guide](https://docs.github.com/en/copilot/using-github-copilot/asking-github-copilot-questions-in-your-ide)

### GitHub Spec-Kit Resources
- [GitHub Spec-Kit Repository](https://github.com/github/spec-kit)
- [Spec-Driven Development Guide](https://github.com/github/spec-kit/blob/main/spec-driven.md)
- [Quick Start Guide](https://github.com/github/spec-kit/blob/main/docs/quickstart.md)
- [Video Overview](https://www.youtube.com/watch?v=a9eR1xsfvHg)
- [Spec-Kit Issues & Support](https://github.com/github/spec-kit/issues)

### Architecture & Patterns
- [Microsoft Architecture Guides](https://learn.microsoft.com/en-us/azure/architecture/)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Domain-Driven Design](https://martinfowler.com/tags/domain%20driven%20design.html)

---

## Quick Reference: Spec-Kit Commands

Once Spec-Kit is initialized in your repository, you have access to these powerful slash commands:

| Command | Purpose | Example |
|---------|---------|---------|
| `/constitution` | Create project principles and development guidelines | `/constitution Create principles for API design and testing` |
| `/specify` | Define WHAT to build (feature specification) | `/specify Add user authentication with JWT tokens` |
| `/plan` | Define HOW to build (tech stack & architecture) | `/plan Use ASP.NET Identity with EF Core and PostgreSQL` |
| `/tasks` | Generate actionable, ordered task list | `/tasks` |
| `/implement` | Execute the task plan systematically | `/implement` |

**Typical Workflow**:
1. Start with `/constitution` (once per project)
2. For each feature: `/specify` → `/plan` → `/tasks` → `/implement`
3. Iterate on specs/plans as needed before implementation

**Installation Reminder**:
```bash
# In your project root
uvx --from git+https://github.com/github/spec-kit.git specify init --here --ai copilot --force
```

---

## Appendix: Spec-Kit Without uvx

If you cannot install uvx or prefer not to use it, here are alternative methods to use Spec-Kit:

### Option 1: Install uv Tool Globally

Install Spec-Kit as a persistent global tool using uv:

```bash
# Install the specify CLI tool globally
uv tool install specify-cli --from git+https://github.com/github/spec-kit.git

# Now you can use it directly (no uvx needed)
specify init --here --ai copilot --force

# Later, to update
uv tool upgrade specify-cli

# To uninstall
uv tool uninstall specify-cli
```

### Option 2: Clone and Run Locally

Clone the Spec-Kit repository and run it directly with Python:

```bash
# Clone the repository
git clone https://github.com/github/spec-kit.git
cd spec-kit

# Create virtual environment
python -m venv .venv
source .venv/bin/activate  # On Windows: .venv\Scripts\activate

# Install dependencies
pip install -e .

# Navigate to your project
cd /path/to/net-users-demo

# Run from the cloned repo
python /path/to/spec-kit/src/specify_cli/__init__.py init --here --ai copilot --force
```

### Option 3: Manual Template Setup

Download and set up templates manually without any tool:

```bash
# In your project root
cd /path/to/net-users-demo

# Download the latest release
curl -L https://github.com/github/spec-kit/archive/refs/heads/main.zip -o spec-kit.zip

# Extract templates (adjust paths as needed)
unzip spec-kit.zip
cp -r spec-kit-main/templates .specify/
cp -r spec-kit-main/scripts .specify/scripts/

# Make scripts executable (Unix-like systems)
chmod +x .specify/scripts/bash/*.sh

# Clean up
rm -rf spec-kit-main spec-kit.zip
```

Then manually configure your AI agent's custom commands by copying the command templates from `.specify/templates/commands/` to your agent's configuration directory (e.g., `.github/copilot/` for GitHub Copilot).

### Option 4: Use Docker (Advanced)

If you have Docker installed:

```bash
# Create a Dockerfile in your project
cat > Dockerfile.specify <<EOF
FROM python:3.11-slim
RUN pip install uv
RUN uv tool install specify-cli --from git+https://github.com/github/spec-kit.git
WORKDIR /workspace
ENTRYPOINT ["specify"]
EOF

# Build the image
docker build -f Dockerfile.specify -t spec-kit .

# Run Spec-Kit commands
docker run --rm -v $(pwd):/workspace spec-kit init --here --ai copilot --force
```

### Troubleshooting

**Issue**: Python not found
- **Solution**: Install Python 3.11+ from [python.org](https://www.python.org/downloads/)

**Issue**: Git not found
- **Solution**: Install Git from [git-scm.com](https://git-scm.com/downloads)

**Issue**: Permission denied on scripts
- **Solution** (Unix-like): Run `chmod +x .specify/scripts/bash/*.sh`
- **Solution** (Windows): Use PowerShell scripts (`.ps1`) or run `Set-ExecutionPolicy -Scope CurrentUser RemoteSigned`

**Issue**: SSL certificate errors on corporate networks
- **Solution**: Try adding `--skip-tls` flag (not recommended for production)

For more help, see the [Spec-Kit Installation Guide](https://github.com/github/spec-kit/blob/main/docs/installation.md) or [open an issue](https://github.com/github/spec-kit/issues/new).

---

## Feedback and Contributions

Found an issue or have suggestions for additional exercises? Please open an issue or submit a pull request!

Happy coding with GitHub Copilot! 🚀
