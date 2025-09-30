# Module 1: Core CRUD & API Design Enhancements

[⬅️ Back to Index](README.md) | [➡️ Next Module: Validation & Error Handling](module-02-validation-error-handling.md)

---

## Overview

This module focuses on completing and enhancing the basic CRUD (Create, Read, Update, Delete) operations for the Users API. You'll implement the missing DELETE endpoint, add advanced features like partial updates and pagination, and learn how to design robust REST APIs.

**Learning Objectives:**
- Complete CRUD operations with proper HTTP semantics
- Implement pagination and filtering for list endpoints
- Handle bulk operations efficiently
- Follow REST API best practices

**Prerequisites:**
- Basic understanding of REST APIs
- Familiarity with ASP.NET Core controllers
- Understanding of HTTP status codes

---

## Exercises

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

## Code Review Before Committing

Before committing your changes, it's essential to perform a thorough local code review. This practice helps catch issues early and ensures code quality.

### VS Code Built-in Tools

**1. Review Changes in Source Control View:**
- Open the Source Control view (⌃⇧G or Ctrl+Shift+G)
- Review each modified file in the "Changes" section
- Click on files to see inline diffs with the previous version
- Look for:
  - Unintended changes or debug code
  - Formatting inconsistencies
  - Missing documentation
  - TODOs or commented-out code

**2. Use the Diff Editor:**
- Right-click a modified file in Source Control view
- Select "Open Changes" to see side-by-side diff
- Review each change carefully:
  - Does it serve the intended purpose?
  - Are there any security concerns?
  - Is the code readable and maintainable?

**3. Check for Errors and Warnings:**
- Open the Problems panel (⌘⇧M or Ctrl+Shift+M)
- Resolve all errors before committing
- Address warnings where appropriate
- Run the build: `dotnet build`

### Using GitHub Copilot for Code Review

**Ask Copilot to Review Your Changes:**

```
Review my recent changes for potential issues:
- Check for security vulnerabilities
- Identify code quality issues
- Suggest improvements for readability
- Verify error handling is comprehensive
```

**Sample Prompts for Specific Reviews:**

```
Review the UsersController for REST API best practices
```

```
Check if my new endpoints follow the existing code style and conventions
```

```
Analyze the error handling in my delete endpoint implementation
```

### Testing Before Commit

**Run the Application:**
```bash
cd net-users-api
dotnet run
```

**Test Your Endpoints:**
- Use the `.http` files in the project
- Test happy paths and error scenarios
- Verify response codes and bodies match expectations
- Check logs for appropriate messages

**Run Tests (if available):**
```bash
dotnet test
```

### Code Review Checklist

Use this checklist before committing:

- [ ] **Functionality**: Does the code work as intended?
- [ ] **Tests**: Are there tests? Do they pass?
- [ ] **Error Handling**: Are edge cases handled?
- [ ] **Logging**: Are operations logged appropriately?
- [ ] **Documentation**: Are XML comments added to public APIs?
- [ ] **Code Style**: Does it follow project conventions?
- [ ] **Security**: Are there any security concerns?
- [ ] **Performance**: Are there obvious performance issues?
- [ ] **Dependencies**: Are new dependencies necessary and appropriate?
- [ ] **Configuration**: Are hardcoded values moved to configuration?
- [ ] **Cleanup**: Is debug code, console logs, or commented code removed?

### Committing Best Practices

**Write Clear Commit Messages:**
```bash
git add .
git commit -m "feat: implement DELETE endpoint for users

- Add DELETE /api/v1/users/{id} endpoint
- Return 204 on success, 404 when not found
- Add logging for delete operations
- Include XML documentation"
```

**Use Conventional Commits:**
- `feat:` for new features
- `fix:` for bug fixes
- `docs:` for documentation changes
- `refactor:` for code refactoring
- `test:` for adding tests
- `chore:` for maintenance tasks

**Atomic Commits:**
- Commit related changes together
- One logical change per commit
- Makes it easier to review and revert if needed

---

## Summary

Congratulations! 🎉 You've completed Module 1. You should now have:
- ✅ A complete CRUD API with all basic operations
- ✅ Advanced features like pagination and filtering
- ✅ Bulk operation support
- ✅ Understanding of REST API design principles

## Next Steps

Ready to make your API more robust? Continue to [Module 2: Validation & Error Handling](module-02-validation-error-handling.md) to learn how to validate input and handle errors gracefully.

---

[⬅️ Back to Index](README.md) | [➡️ Next Module: Validation & Error Handling](module-02-validation-error-handling.md)
